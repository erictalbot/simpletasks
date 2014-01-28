(function () {

    // First off, you add qunit MVC by using manage nuget 
    // Then see documentation at : http://api.qunitjs.com/

    QUnit.config.testTimeout = 10000;


    //module('SIMPLETASK creation tests'); you may use module to organize your test, i just find them a bit redundant.

    //----------------------------------------------------------------------------------
    // Create tests
    //----------------------------------------------------------------------------------
    //Arrange
    var postUrl = '/api/Task/';
    var jSonPostContent = '{"Description":"description","DueDate":"25-11-2012","Completion":"0","SimpleTaskId":"0"}';
    var allPostTests = [
        testPostReturnSuccess,
        testPostReturnOurObjectDescription,
        testPostReturnASimpleTaskId
    ];
    //Assert
    asyncTest('We can create SIMPLETASK successfully.' + postUrl,
            CreatePostTest(postUrl, jSonPostContent, allPostTests)
    );

    //----------------------------------------------------------------------------------
    // Delete tests
    //----------------------------------------------------------------------------------
    // Arrange 
    var simpleTaskId;
    function CreateADummySimpleTask(url, payload) {
        $.ajax({
            url: url,
            type: 'POST',
            data: payload,
            async: false,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (result) {                        // The fact that we hit success , means we received HTTP status 200
                simpleTaskId = result.simpleTaskId
            },
            error: function (result) {                          // HTTP Status 400
                ok(false, 'We cannot create a SIMPLETASK to delete' + result.status);
            }
        });
    };

    //Act
    $.when(CreateADummySimpleTask(postUrl,jSonPostContent)).done(function() {
        var allDeleteTests = [
            testDeleteExistingSimpleTaskReturnSuccess,
            testDeleteReturnsNoObject,
        ];
        // Assert
        asyncTest('We can delete SIMPLETASK successfully.',    
                CreateDeleteTest('/api/Task?id='+simpleTaskId, '', allDeleteTests)
        );
    });

    asyncTest('We get error if we try to delete non-existent SimpleTask.',
        CreateDeleteExpectFailureTest('/api/Task?id=999999', '', [testDeleteNonExistentSimpleTaskReturnsError])
    );

    //----------------------------------------------------------------------------------
    // Edit tests
    //----------------------------------------------------------------------------------
    $.when(CreateADummySimpleTask(postUrl, jSonPostContent)).done(function () {
        var jSonModifiedPostContent = '{"Description":"description2","DueDate":"25-12-2012","Completion":"1","SimpleTaskId":' + simpleTaskId + '}';
        var allEditTests = [
          testEditExistingSimpleTaskReturnSuccess,
          testEditSimpleTaskReturnsAModifiedCopyOfTheObject,
        ];
        // Assert
        asyncTest('We can edit existing SIMPLETASK successfully.',
           CreatePostTest('/api/Task/', jSonModifiedPostContent, allEditTests)
        );
    });

    var jSonModifiedPostContent = '{"Description":"EDITNONEXISTENT","DueDate":"25-12-2012","Completion":"1","SimpleTaskId":99999}';
    asyncTest('Edit non existent will create a new task.',
       CreatePostTest('/api/Task/', jSonModifiedPostContent, [testEditNonExistentSimpleTaskReturnsANewSimpleTask])
    );

    //----------------------------------------------------------------------------------
    // Get all 
    //----------------------------------------------------------------------------------
    asyncTest('GET on /api/task returns a list of SimpleTask',
        CreateGetTest('/api/Task/', [testGetAllReturnsASimpleTaskList])
    );

    //----------------------------------------------------------------------------------
    // TearDown & Startup Note :
    // While we may implement something here to wipe out the whole table before and after
    // we run the tests. I just don't think it is the best place to do it. 
    //----------------------------------------------------------------------------------
}());