
function ExpectSuccess(url, payload, type, testSuccesses) {
    $.ajax({
        url: url,
        type: type,
        data: payload,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (result) {                        // The fact that we hit success , means we received HTTP status 200
            for (var i = 0; i < testSuccesses.length; i++)
                testSuccesses[i](result, payload);
            start();
        },
        error: function (result) {                          // HTTP Status 400
            ok(false,type + ' on ' + url + ' failed with status= ' + result.status);
            start();
        }
    });
};

function ExpectFailure(url, payload, type, testFailures) {
    $.ajax({
        url: url,
        type: type,
        data: payload,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (result) {                        // The fact that we hit success , means we received HTTP status 200
            ok(false, type + ' on ' + url + ' succeeded with status= ' + result.status);
            start();
        },
        error: function (result) {                          // HTTP Status 400
            for (var i = 0; i < testFailures.length; i++)
                testFailures[i](result, payload);
            start();
        }
    });
};


//-------------------------------------------------------------------
// Assert methods
//-------------------------------------------------------------------
function testPostReturnSuccess(valueReceivedFromController, valueSentToController) {
    ok(true, "We can post :" + JSON.stringify(valueSentToController));
}

function testPostReturnOurObjectDescription(valueReceivedFromController, valueSentToController) {
    // You might wonder why the value received from the controller has an upper case D ( in description ) 
    // and the value sent has a lower case d ( in description). This is only because of the context in which the 
    // object is used. For example in C#, properties use Pascal Case, therefore when we send object to the controller
    // we take good care of creating an object with Pascal Case properties. Javascipt 
    // uses camel Case, therefore when the controller sends us object it uses camel Case properties.
    // http://en.wikipedia.org/wiki/Naming_convention_(programming)#JavaScript
    // http://stackoverflow.com/questions/5543490/json-naming-convention
    ok(JSON.parse(valueSentToController).Description == valueReceivedFromController.description, "The object received have the same description as the one we sent.");
}

function testPostReturnASimpleTaskId(valueReceivedFromController, valueSentToController) {
    // SimpleTaskId is critical for knockout MVVM. Each object stored in the view model keeps a SimpleTaskId
    // that uniquely identifies the record in the database.
    ok(valueReceivedFromController.simpleTaskId != JSON.parse(valueSentToController).SimpleTaskId, "Returned object have a SimpleTaskId different than the default one we sent.");
}

function testDeleteExistingSimpleTaskReturnSuccess(valueReceivedFromController, valueSentToController) {
    ok(true, "Delete existing task returns success.");
}

function testDeleteReturnsNoObject(valueReceivedFromController, valueSentToController) {
    ok(valueReceivedFromController==null,"No object is received when deleting");
}

function testDeleteNonExistentSimpleTaskReturnsError(valueReceivedFromController, valueSentToController) {
    ok(valueReceivedFromController.status == '404', "Controller sent us back a 404 error");
}

function testEditExistingSimpleTaskReturnSuccess(valueReceivedFromController, valueSentToController) {
    ok(true, "Editing existing task returns success");
}

function testEditSimpleTaskReturnsAModifiedCopyOfTheObject(valueReceivedFromController, valueSentToController) {
    ok(valueReceivedFromController.description == "description2", "Description was changed.");
    ok(new Date(valueReceivedFromController.dueDateMilliSeconds).getMonth() == 11, "Duedate was changed successfully");
}
function testEditNonExistentSimpleTaskReturnsANewSimpleTask(valueReceivedFromController, valueSentToController) {
    ok(valueReceivedFromController.description == 'EDITNONEXISTENT', "Description ok");
    ok(valueReceivedFromController.simpleTaskId != 99999, "SimpleTaskId ok");
}

function testGetAllReturnsASimpleTaskList(valueReceivedFromController, valueSentToController) {
    ok(valueReceivedFromController.length > 0, "We received a list of something");
    ok(valueReceivedFromController[0].description != "", "Which contains a description field.");
}

//-------------------------------------------------------------------
// Test creation methods
//-------------------------------------------------------------------
var CreateDeleteTest = function (url, payload, tests) {
    return function () { ExpectSuccess(url, payload, 'DELETE', tests); };
};

var CreateDeleteExpectFailureTest = function (url, payload, tests) {
    return function () { ExpectFailure(url, payload, 'DELETE', tests); };
};

var CreatePostTest = function (url, payload, tests) {
    return function () { ExpectSuccess(url, payload, 'POST', tests); };
};

var CreateGetTest = function (url, tests) {
    return function () { ExpectSuccess(url, '', 'GET', tests); };
};
