var slider = document.getElementById("myRange");

var tableBody = document.getElementById("tableBody");

var changes = Array.from(tableBody.querySelectorAll('tr'));

for (let i = 0; i < changes.length; i++) {
    changes[i].onchange = startProcess;
}

function startProcess (e) {
    prepareTableState();
    var input = e.target;
    var koef = CalculateK(input);
    CalculateNewValues(koef,input)
    console.log(koef);
}

function prepareTableState () {
    tableBody.querySelectorAll('#correction').forEach(x=>x.innerText=0);
    var newValues = Array.from(tableBody.querySelectorAll('#newValue'));

    if (+newValues[0].innerText > 0) {
        newValues.forEach(x=>x.parentNode.previousSibling.previousSibling.firstChild.innerText=x.innerText);
    }
}

function CalculateK(input) {
    var row = input.parentNode.parentNode;
    var quantityElement = row.querySelector('#quantity');
    var quantity = Number(quantityElement.innerText);
    var newQuantity = quantity + Number(input.value);
    row.querySelector('#newValue').innerText=newQuantity.toFixed(2);
    row.querySelector('#correction').innerText=input.value;
    var koef = (100-newQuantity)/(100-quantity);

    return koef;
}

function CalculateNewValues(koef, input) {
    var values = Array.from(tableBody.querySelectorAll('#quantity'));
    values=values.filter(x=>x.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.firstChild.getAttribute("name")!=input.getAttribute("name"));

    values.forEach(x=>x.parentNode.nextSibling.nextSibling.firstChild.innerText=(Number(x.innerText)*koef).toFixed(2));
}






// Update the current slider value (each time you drag the slider handle)
// slider.oninput = function (e) {
//    // e.preventDefault();
    
//     console.log(this.value);
// }
// var arr = [];
// var json;
// myForm.onchange = function (e) {
//     e.preventDefault();
//      var inputs = myForm.querySelectorAll('input');
//      inputs.forEach(element => {
//          console.log(element.getAttribute('name'));
//         console.log(element.value);
//         var name = element.getAttribute('name');
//         var value = `${element.value}`;
//         var oper = { name, value };
//         arr.push(oper);
//         myForm.querySelectorAll('#correction')
//         .forEach(x=>x.innerHTML = x.previousElementSibling.previousElementSibling.value);
//      });
//      json= JSON.stringify(arr);
//      console.log(json);
//  }
var arr = [];
var json;
// tableBody.onchange = function (e) {
//     e.preventDefault();
//      var inputs = tableBody.querySelectorAll('input');
//      inputs.forEach(element => {
//          console.log(element.getAttribute('name'));
//         console.log(element.value);
//         var name = element.getAttribute('name');
//         var value = `${element.value}`;
//         var oper = { name, value };
//         arr.push(oper);
//         tableBody.querySelectorAll('#correction')
//         .forEach(x=>x.innerHTML = x.parentNode.parentNode.querySelector('input').value);
//         tableBody.querySelectorAll('#newValue')
//         .forEach(x=>x.innerHTML = parseInt(x.parentNode.parentNode.querySelector('#quantity').innerHTML) +
//         parseInt(x.parentNode.parentNode.querySelector('#quantity').innerHTML) * parseInt(x.parentNode.parentNode.querySelector('#correction').innerHTML)/100);
//      });
//      json= JSON.stringify(arr);
//      console.log(json);
//  }

