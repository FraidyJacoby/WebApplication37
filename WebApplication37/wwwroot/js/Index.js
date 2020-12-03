$(() => {

    function LoadTable() {
        $(".tbody tr").remove();
        $.get(`/home/getpeople`, function (obj) {
            obj.people.forEach(p => $(".tbody").append(BuildRow(p.firstName, p.lastName, p.age, p.id)))
        })
    }

    function BuildRow(firstName, lastName, age, id) {
        return `<tr>
                <td>${firstName}</td>
                <td>${lastName}</td>
                <td>${age}</td>
                <td><button class="btn btn-warning edit-button" data-id="${id}" data-first-name="${firstName}" 
    data-last-name="${lastName}" data-age="${age}">Edit</button></td>
                <td><button class="btn btn-danger delete-button" data-id="${id}" data-first-name="${firstName}" 
    data-last-name="${lastName}" data-age="${age}">Delete</button></td>
                </tr>`;
    }

    LoadTable();

    $("#add-person").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();
        $.post(`/home/addperson`, ({ firstName, lastName, age }), function () {
            LoadTable();
        })
        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');
    })

    $(".table").on('click', ".edit-button", function () {
        ClearModal();

        $("#edit-first-name").val($(this).data("first-name"));
        $("#edit-last-name").val($(this).data("last-name"));
        $("#edit-age").val($(this).data("age"));
        $("#save").data("id", $(this).data("id"));

        $("#edit-modal").modal();
    })

    $("#save").on('click', function () {
        const id = $(this).data("id");
        const firstName = $("#edit-first-name").val();
        const lastName = $("#edit-last-name").val();
        const age = $("#edit-age").val();

        $.post(`/home/editperson`, { id, firstName, lastName, age }, function () {
            LoadTable();
        })
    })

    function ClearModal() {
        $("#edit-first-name").val('');
        $("#edit-last-name").val('');
        $("#edit-age").val('');
    }

    $(".table").on('click', ".delete-button", function () {
        const id = $(this).data("id");
        $.post(`/home/deleteperson`, {id}, function () {
            LoadTable();
        })
    })
})