new Pikaday({
    field: document.querySelector('[name=PublicationDate]'),
    firstDay: 1,
    format: 'D/M/YYYY',
    minDate: new Date(),
    maxDate: new Date(2999, 12, 31),
    i18n: {
        previousMonth: 'Mois précédent',
        nextMonth: 'Mois suivant',
        months: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
        weekdays: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'],
        weekdaysShort: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam']
    },
    toString(date, format) {
        const day = ("0" + date.getDate()).slice(-2);
        const month = ("0" + (date.getMonth() + 1)).slice(-2);
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }
});

var editor = document.querySelector('.md-editor');
var result = document.querySelector('.postdata');
var resultValue = document.querySelector('#Content');

//showdown.extension('highlightjs', function () {
//    return [{
//        type: 'output',
//        filter: function (text, converter, options) {
//            var left = '<pre><code\\b[^>]*>';
//            var right = '</code></pre>';
//            var flags = 'g';

//            var replacement = function (wholeMatch, match, left, right) {
//                var languageRegex = 'code class="([a-zA-Z]+) language-';
//                var languageMatch = left.match(languageRegex);
//                if (languageMatch && languageMatch.length > 0) {
//                    var language = languageMatch[1];
//                }

//                return "<pre" + (language ? " class=\"" + "brush: " + language + "\"" : "") + ">" + match + "\n</pre>";
//            };

//            return showdown.helper.replaceRecursiveRegExp(text, replacement, left, right, flags);
//        }
//    }];
//});

//var converter = new showdown.Converter({ extensions: ['highlightjs'] })
var converter = new showdown.Converter();

var redrawResult = debounce(function () {
    var html = converter.makeHtml(editor.value);
    result.innerHTML = html;
    resultValue.value = html;
    setTimeout(function () { Prism.highlightAll(); }, 0);
}, 250);

editor.addEventListener('keydown', redrawResult);
redrawResult();

function debounce(func, wait, immediate) {
    var timeout;

    return function () {
        var context = this, args = arguments;
        var later = function () {
            timeout = null;
            func.apply(context, args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
};