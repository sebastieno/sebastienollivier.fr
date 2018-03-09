new Pikaday({
    field: document.querySelector('[name=PublicationDate]'),
    firstDay: 1,
    format: 'D/M/YYYY',
    minDate: new Date(2010, 0, 1),
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

var converter = new showdown.Converter();

var redrawResult = debounce(function () {
    var html = converter.makeHtml(editor.value);
    result.innerHTML = html;
    resultValue.value = html;
    setTimeout(function () { Prism.highlightAll(); }, 0);
}, 400);

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