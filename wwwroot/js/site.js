// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//set the question detail editable on edit click
var oldVals = [];


function SwitchEnabled() {
    var reset = false;
    var elements = document.getElementsByClassName('question');
    if (elements[0].disabled === false && confirm("You didn't save your changes, are you sure you want to cancel?") == false) {
        return false;
    }
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].disabled === false) {
            elements[i].disabled = true;
            oldVals.push(elements[i].val())
        }
        else {
            elements[i].val = oldVals[i];
            elements[i].disabled = false;
            reset = true;
        }
    }
    if (reset) {
        oldVals = [];
    }
    return false;
}