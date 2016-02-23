var gulp = require("gulp");
var browserSync = require('browser-sync').create();
var shell = require('gulp-shell');
var autoprefixer = require('gulp-autoprefixer');
var cssnano = require("gulp-cssnano");
var less = require("gulp-less");
var concat = require("gulp-concat");
var notify = require("gulp-notify");

gulp.task('compile-less', function () {
    return gulp.src('./Styles/**/*.less')
        .pipe(less()).on('error', notify.onError({
            message: "<%= error.message %>",
            title: "Erreur dans le less"
        }))
        .pipe(autoprefixer())
        .pipe(cssnano())
        .pipe(gulp.dest('./wwwroot/css'));
});

var handlerError = function (err) {
    notify.onError(err.message);
    this.emit('end');
};

gulp.task('copy-images', function() {
    return gulp.src('./Styles/Images/**')
        .pipe(gulp.dest('./wwwroot/images'));
});

gulp.task('dnxwatch', function () {
    gulp.src('')
       .pipe(shell(['dnx-watch --project ./project.json --dnx-args web ASPNET_ENV=Development']));
});

gulp.task('default', ['dnxwatch', 'compile-less', 'copy-images'], function () {
    browserSync.init({
        files: ['./wwwroot/**/*', './Views/*.cshtml'],
        port: '5000',
        server: {
            baseDir: './wwwroot/'
        }
    });

    gulp.watch('./Styles/**/*.less', ['compile:styles']);
});