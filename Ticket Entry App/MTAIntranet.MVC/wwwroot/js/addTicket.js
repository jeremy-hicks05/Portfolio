$(document).ready(function () {

    if ($('#Category').val() != null) {
        showSubTypes();
        loadSubTypes();
    }

    $('#Category').change(function () {
        hideAllAdditionalFields();
        showSubTypes();
        loadSubTypes();
    });

    $('#SubType').change(function () {
        subTypeRequiresApproval();
        showAllRequiredFileds($('#SubType').val());
    });

    $('#Impact').change(function () {
        if ($('#SubType').val().includes('Password Reset')) {

        }
        else if ($('#SubType').val().includes('New User')) {

        }
        else {
            showSummary();
        }
    });

    $('#submit').click(function () {
        buildSummary();
    });
});

function loadSubTypes() {
    $('#SubType').empty();
    $('#SubType').append("<option value= \"none\" selected disabled>--Please Select a SubType --</option>");
    $.ajax({
        url: '/Tickets/GetTicketSubTypes/' + $('#Category').find(":selected").val(),
        success: function (response) {
            for (let i = 0; i < response.length; i++) {
                $('#SubType').append("<option value= \"" + response[i] + "\">" + response[i] + "</option>");
            }
        },
        error: function (response) {
            alert(response);
        }
    });
}

function subTypeRequiresApproval() {
    $.ajax({
        url: '/Tickets/GetTicketSubTypeApproval/' + $('#Category').find(":selected").val() + "/" + $('#SubType').find(":selected").val(),
        success: function (response) {
            if (response === "Yes") {
                $('#ApprovalState').val("Needs Approval");
            }
            else {
                $('#ApprovalState').val("Does not need approval");
            }
        },
        error: function (response) {
            alert(response);
        }
    });
}

function buildSummary() {
    if ($('#userid').val() !== "") {
        $('#Summary').val("\n" + $('#Summary').val() + "<br />" + ("User ID: " + $('#userid').val()));
    }
    if ($('#group').val() !== undefined && $('#group').val() !== "") {
        $('#Summary').val("\n" + $('#Summary').val() + "<br />" + ("Group: " + $('#group').val()));
    }
}

function showSubTypes() {
    $('#SubType').removeAttr("hidden");
    $('#SubTypeLabel').removeAttr("hidden");
}

function showSummary() {
    $('#Summary').empty();
    $('#Summary').removeAttr("hidden");
    $('#SummaryLabel').removeAttr("hidden");
}

function showImpacts() {
    $('#Impact').removeAttr("hidden");
    $('#ImpactLabel').removeAttr("hidden");
    $('#Impact option:eq(0)').prop('selected', true);
}

function showUserId() {
    $('#userid').removeAttr("hidden");
    $('#useridlabel').removeAttr("hidden");
}

function showGroup() {
    $('#group').removeAttr("hidden");
    $('#grouplabel').removeAttr("hidden");
}

function hideUserId() {
    $('#userid').empty();
    $('#userid').attr("hidden", true);
    $('#useridlabel').attr("hidden", true);
}

function hideGroup() {
    $('#group').empty();
    $('#group').attr("hidden", true);
    $('#grouplabel').attr("hidden", true);
}

function hideImpact() {
    $('#Impact').attr("hidden", true);
    $('#ImpactLabel').attr("hidden", true);
}

function hideSummary() {
    $('#Summary').empty();
    $('#Summary').attr("hidden", true);
    $('#SummaryLabel').attr("hidden", true);
}

function showAllRequiredFileds(subType) {
    hideAllAdditionalFields();

    if (subType.includes('Password Reset')) {
        $('#userid').attr("required", true);
        hideSummary();
        showImpacts();
        showUserId();
    }
    else if (subType.includes('New User')) {
        $('#userid').attr("required", true);
        hideSummary();
        showImpacts();
        showUserId();
        showGroup();
    }
    else {
        showImpacts();
    }
}

function hideAllAdditionalFields() {
    $('#userid').removeAttr("required");
    hideSummary();
    hideImpact();
    hideUserId();
    hideGroup();
}