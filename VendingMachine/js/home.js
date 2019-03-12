$(document).ready(function () {

    loadItems();

});

//Get and display candy
function loadItems() {

    var contentRows = $('#contentRows');

    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function (data, status) {
            $.each(data, function (index, item) {
                var id = item.id;
                var name = item.name;
                var price = item.price;
                var quantity = item.quantity;

                var row = '<ul>';
                    row += '<button type="button" id="candyButton"  style="width: 180px">';
                    row += '<p id="candyId' + id + '" class="text-left itemNumber">' + id + '</p>';
                    row += '<p id="candyName' + id + '">' + name + '</p>';
                    row += '<p id="candyPrice' + id + '">' + '$' + price.toFixed(2) + '</p>';
                    row += '<p id="candyQuantity' + id + '">' + 'Quantity left: ' + quantity + '</p>';
                    row += '</button>';
                    row += '</ul>';
                contentRows.append(row);
            });
        },
        error: function() {
            alert('Error: Problem connecting to web service.');
        }
    });
}

//Make purchase button
$('#makePurchase').on('click', function() {
    $('#changeField').val('');
    var candyNumber = $('#itemField').val();
    var purchaseMoney = $('#totalField').val();
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/money/' + purchaseMoney + '/item/' + candyNumber,
        success: function() {
            candyCost = $('#candyPrice' + candyNumber).text();
            candyCostReal = Math.round(parseFloat(candyCost.substring(1,candyCost.length)) * 100);
            purchaseMoney = Math.round(parseFloat(purchaseMoney) * 100);
            var updatedBalance = ((purchaseMoney - candyCostReal) / 100).toString();
            money = parseFloat(updatedBalance);
            $('#totalField').val(updatedBalance);
            $('#messageField').val('Thank You!!!');
            $('#contentRows').empty();
            updateMoney();
            loadItems();
          },
          error: function(jqXHR) {
            $('#messageField').val(JSON.parse(jqXHR.responseText).message);
          }
    });
})

//Add money buttons
var money = 0.00; 

$('#addDollar').on('click', function() {
   money+=1.00;
   updateMoney();});

$('#addQuarter').on('click', function() {
    money+=0.25;
    updateMoney();});

$('#addDime').on('click', function() {
    money+=0.10;
    updateMoney();});

$('#addNickel').on('click', function() {
    money+=0.05;
    updateMoney();});

//Update money in total field  
function updateMoney(){
    $('#totalField').val(money.toFixed(2));};

//Select candy
$(document).on('click', '#candyButton', function() {
    $('#itemField').val($(this).find('.itemNumber').text());});

// CHANGE RETURN BUTTON
 $('#changeReturn').click(function() {
    var balanceInPennies = Math.round(parseFloat($('#totalField').val()) * 100);
    var quarters = Math.floor(balanceInPennies / 25);
    balanceInPennies %= 25;
    var dimes = Math.floor(balanceInPennies / 10);
    balanceInPennies %= 10;
    var nickels = Math.floor(balanceInPennies / 5);
    balanceInPennies %= 5;
    var change = {"quarters":quarters, "dimes":dimes, "nickels":nickels,"pennies":balanceInPennies};
    displayChange(change);
    $('#totalField').val('0.00');
    $('#itemField').val('');
    $('#messageField').val('')
    $('#contentRows').empty();
    loadItems();
    money = 0.00;
});

//Display change in change field
function displayChange(change) {
    var changeMessage = "" + change.quarters + " Quarters ";
        changeMessage += change.dimes + " Dimes ";
        changeMessage += change.nickels + " Nickels ";
        changeMessage += change.pennies + " Pennies";
        $('#changeField').val(changeMessage);
}




