// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//set the question detail editable on edit click
var oldVals = [];
var oldAnswerVals = [];


function SwitchEnabled() {
    var reset = false;

    var show = document.getElementById("q_update");
    var hide = document.getElementById("q_edit");
    show.hidden = false;
    hide.hidden = true;

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

function SwitchAnswer(className) {
    var reset = false;
    var elements = document.getElementsByClassName(className);
    if (elements[0].disabled === false && confirm("You didn't save your changes, are you sure you want to cancel?") == false) {
        return false;
    }
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].disabled === false) {
            elements[i].disabled = true;
            oldAnswerVals.push(elements[i].val())
        }
        else {
            elements[i].val = oldAnswerVals[i];
            elements[i].disabled = false;
            reset = true;
        }
    }
    if (reset) {
        oldAnswerVals = [];
    }

    var editButton = document.getElementById(className);
    var updateClass = className + "_update";
    var updateButton = document.getElementById(updateClass);
    if (editButton.hidden == true) {
        editButton.hidden = false;
        updateButton.hidden = true;
    }
    else {
        editButton.hidden = true;
        updateButton.hidden = false;
    }

    return false;
}

function DisplayLogin() {
    var show = document.getElementById("DisplayLogin");
    var hide = document.getElementById("LoginHelper");
    show.hidden = false;
    hide.hidden = true;
    return true;
}