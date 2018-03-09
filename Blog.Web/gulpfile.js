var gulp = require("gulp");
var browserSync = require('browser-sync');
var shell = require('gulp-shell');
var autoprefixer = require('gulp-autoprefixer');
var cssnano = require("gulp-cssnano");
var less = require("gulp-less");
var concat = require("gulp-concat");
var notify = require("gulp-notify");
var watch = require("gulp-watch");

gulp.task('compile-less', function () {
    return gulp.src('./Styles/**/*.less')
        .pipe(less()).on('error', notify.onError({
            message: "<%= error.message %>",
            title: "Erreur dans le less"
        }))
        .pipe(autoprefixer())
        .pipe(cssnano())
        .pipe(gulp.dest('./wwwroot/styles'));
});

gulp.task('copy-syntaxhighlighter:css', function () {
    return gulp.src('./Styles/SyntaxHighlighter/**/shCoreRDark.css')
        .pipe(cssnano())
        .pipe(concat('syntaxhighlighter.css'))
        .pipe(gulp.dest('./wwwroot/styles'));
});


gulp.task('copy-images', function () {
    return gulp.src('./Styles/Images/**')
        .pipe(gulp.dest('./wwwroot/images'));
});

gulp.task('less-watch', ['compile-less'], function () {
    gulp.src("./wwwroot/styles/**/*.css")
        .pipe(browserSync.stream());
});

gulp.task('default', ['compile-less', 'copy-images'], function () {
    browserSync.init(null, {
        proxy: "https://localhost:44322"
    });

    gulp.watch('./Styles/**/*.less', ['less-watch']);
    gulp.watch('./Views/**/*.cshtml', function () {
        browserSync.reload();
    });
});