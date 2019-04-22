$(document).ready(function () {

    loadDvds();

});

function loadDvds() {

    clearDvdTable();
    var contentRows = $('#contentRows');

    $.ajax ({
        type: 'GET',
        url: 'http://localhost:61683/dvds',
        success: function (data, status) {    
            $.each(data, function (index, item) {
                var id = item.dvdId;
                var title = item.title;
                var releaseYear = item.realeaseYear;
                var director = item.director;
                var rating = item.rating;
                var notes = item.notes;

                var row = '<tr>';
                    row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td>' + notes + '</td>';
                    row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                    row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                    row += '</tr>';
                contentRows.append(row);
            });
        },
        error: function() {
            alert('Error: Problem connecting to web service.');
        }
    });
}

$('#search-button').click(function () {

    var searchTerm = $('#search-list').val();
    var search = $('#search-text').val();

    if(searchTerm == null || search == "")
   {
     alert("Please select a search category and term");
     displayList();
   }

    clearDvdTable();
    var contentRows = $('#contentRows');

    if (searchTerm == 'Id') {   
    
    $.ajax ({
        type: 'GET',
        url: "http://localhost:61683/dvd/" + search,
        success: function (data, status) {    
                var id = data.dvdId;
                var title = data.title;
                var releaseYear = data.realeaseYear;
                var director = data.director;
                var rating = data.rating;
                var notes = data.notes;

                var row = '<tr>';
                    row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td>' + notes + '</td>';
                    row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                    row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                    row += '</tr>';
                contentRows.append(row);
       
    },
        error: function() {
        alert('Error: Problem connecting to web service.');
        }

    });
}

    if (searchTerm == 'Title') {

    $.ajax ({
        type: 'GET',
        url: "http://localhost:61683/dvds/title/" + search,
        success: function (data, status) {    
            $.each(data, function (index, item) {
                var id = item.dvdId;
                var title = item.title;
                var releaseYear = item.realeaseYear;
                var director = item.director;
                var rating = item.rating;
                var notes = item.notes;

                var row = '<tr>';
                    row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td>' + notes + '</td>';
                    row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                    row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                    row += '</tr>';
                contentRows.append(row);
            });
        },
        error: function() {
        alert('Error: Problem connecting to web service.');
        }

    });
}

    if (searchTerm == 'Release Year') {   
    
    if (isNaN(search)){
        alert('Please enter a valid year')
    }
    else{

    $.ajax ({
        type: 'GET',
        url: "http://localhost:61683/dvds/year/" + search,
            success: function (data, status) {    
            $.each(data, function (index, item) {
                var id = item.dvdId;
                var title = item.title;
                var releaseYear = item.realeaseYear;
                var director = item.director;
                var rating = item.rating;
                var notes = item.notes;

                var row = '<tr>';
                    row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td>' + notes + '</td>';
                    row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                    row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                    row += '</tr>';
                contentRows.append(row);
            });
        },
        error: function() {
        alert('Error: Problem connecting to web service.');
        }

    });
    }
}

    if (searchTerm == 'Director Name') {   
        
        $.ajax ({
            type: 'GET',
            url: "http://localhost:61683/dvds/director/" + search,
                success: function (data, status) {    
                $.each(data, function (index, item) {
                    var id = item.dvdId;
                    var title = item.title;
                    var releaseYear = item.realeaseYear;
                    var director = item.director;
                    var rating = item.rating;
                    var notes = item.notes;

                    var row = '<tr>';
                        row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                        row += '<td>' + releaseYear + '</td>';
                        row += '<td>' + director + '</td>';
                        row += '<td>' + rating + '</td>';
                        row += '<td>' + notes + '</td>';
                        row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                        row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                        row += '</tr>';
                    contentRows.append(row);
            });
        },
        error: function() {
        alert('Error: Problem connecting to web service.');
        }

    });
}

    if (searchTerm == 'Rating') {   
        
        $.ajax ({
            type: 'GET',
            url: "http://localhost:61683/dvds/rating/" + search.toUpperCase(),
                success: function (data, status) {    
                $.each(data, function (index, item) {
                    var id = item.dvdId;
                    var title = item.title;
                    var releaseYear = item.realeaseYear;
                    var director = item.director;
                    var rating = item.rating;
                    var notes = item.notes;

                    var row = '<tr>';
                        row += '<td><button class="btn btn-default" onclick="showDetailForm(' + id + ')">' + title + '</button></td>';
                        row += '<td>' + releaseYear + '</td>';
                        row += '<td>' + director + '</td>';
                        row += '<td>' + rating + '</td>';
                        row += '<td>' + notes + '</td>';
                        row += '<td><button class="btn btn-success" onclick="showEditForm(' + id + ')">Edit</button></td>';  
                        row += '<td><button class="btn btn-danger" onclick="deleteContact(' + id + ')">Delete</button></td>';                  
                        row += '</tr>';
                    contentRows.append(row);
            });
        },
        error: function() {
        alert('Error: Problem connecting to web service.');
        }

    });
}
});



function deleteContact(dvdId) {

    var remove = confirm("Are you sure you would like to delete this entry?");

    if (remove == true) {

    $.ajax ({
        type: 'DELETE',
        url: "http://localhost:61683/dvd/" + dvdId,
        success: function () {
            loadDvds();
        }
    });
}

}

$('#add-button').click(function () {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#add-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

    $.ajax({
        type: 'POST',
        url: 'http://localhost:61683/dvd',
        data: JSON.stringify({
            title: $('#add-title').val(),
            realeaseYear: $('#add-release').val(),
            director: $('#add-director').val(),
            rating: $('#add-rating').val(),
            notes: $('#add-notes').val(),
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function() {

            $('#errorMessages').empty();

            hideAddForm();
            loadDvds();
        },
        error: function() {
            $('#errorMessages')
               .append($('<li>')
               .attr({class: 'list-group-item list-group-item-danger'})
               .text('Error calling web service.  Please try again later.'));
        }
    });
});

$('#edit-button').click(function () {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        var realId = document.getElementById('edit-dvd-id').value

    $.ajax({
       type: 'PUT',
       url: 'http://localhost:61683/dvd/' + realId,
       data: JSON.stringify({
         dvdId: realId,
         title: $('#edit-title').val(),
         realeaseYear: $('#edit-release').val(),
         director: $('#edit-director').val(),
         rating: $('#edit-rating').val(),
         notes: $('#edit-notes').val(),
       }),
       headers: {
         'Accept': 'application/json',
         'Content-Type': 'application/json'
       },
       'dataType': 'json',
        success: function() {

            $('#errorMessages').empty();
            hideEditForm();
            loadDvds();
       },
       error: function() {
         $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service.  Please try again later.'));
       }
   })
});

function showEditForm(dvdId) {

    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:61683/dvd/' + dvdId,
        success: function(data, status) {
              $('#edit-title').val(data.title);
              $('#edit-release').val(data.realeaseYear);
              $('#edit-director').val(data.director);
              $('#edit-rating').val(data.rating);
              $('#edit-notes').val(data.notes);
              $('#edit-dvd-id').val(data.dvdId);
          },
          error: function() {
            $('#errorMessages')
               .append($('<li>')
               .attr({class: 'list-group-item list-group-item-danger'})
               .text('Error calling web service.  Please try again later.'));
          }
    });
    $('#dvdTableDiv').hide();
    $('#addFormDiv').hide();
    $('#editFormDiv').show();
}

function showDetailForm(dvdId) {

    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:61683/dvd/' + dvdId,
        success: function(data, status) {
              $('#detail-title').val(data.title);
              $('#detail-release').val(data.realeaseYear);
              $('#detail-director').val(data.director);
              $('#detail-rating').val(data.rating);
              $('#detail-notes').val(data.notes);
              $('#detail-dvd-id').val(data.dvdId);
          },
          error: function() {
            $('#errorMessages')
               .append($('<li>')
               .attr({class: 'list-group-item list-group-item-danger'})
               .text('Error calling web service.  Please try again later.'));
          }
    });
    $('#dvdTableDiv').hide();
    $('#addFormDiv').hide();
    $('#editFormDiv').hide();
    $('#detailFormDiv').show();
}



function hideEditForm() {

    $('#errorMessages').empty();

    $('#edit-title').val('');
    $('#edit-release').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');
    $('#editFormDiv').hide();
    $('#dvdTableDiv').show();
}

function showAddForm() {

    $('#errorMessages').empty();    
    $('#dvdTableDiv').hide();
    $('#editFormDiv').hide();
    $('#addFormDiv').show();
}

function hideDetailForm() {

    $('#errorMessages').empty();

    $('#detail-title').val('');
    $('#detail-release').val('');
    $('#detail-director').val('');
    $('#detail-rating').val('');
    $('#detail-notes').val('');
    $('#detailFormDiv').hide();
    $('#dvdTableDiv').show();
}

function hideAddForm() {

    $('#errorMessages').empty();

    $('#add-title').val('');
    $('#add-release').val('');
    $('#add-director').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');
    $('#addFormDiv').hide();
    $('#dvdTableDiv').show();
}

function clearDvdTable() {
    $('#contentRows').empty();
}

function checkAndDisplayValidationErrors(input) {
    // clear displayed error message if there are any
    $('#errorMessages').empty();
    // check for HTML5 validation errors and process/display appropriately
    // a place to hold error messages
    var errorMessages = [];

    // loop through each input and check for validation errors
    input.each(function() {
        // Use the HTML5 validation API to find the validation errors
        if(!this.validity.valid)
        {
            var errorField = $('label[for='+this.id+']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    // put any error messages in the errorMessages div
    if (errorMessages.length > 0){
        $.each(errorMessages,function(index,message){
            $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
        });
        // return true, indicating that there were errors
        return true;
    } else {
        // return false, indicating that there were no errors
        return false;
    }
}