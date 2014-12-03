angular.module('app').controller('todoController', [
    '$scope',
    '$http',
    function($scope, $http) {
        $http.post('/todo/GetAll', {}).
            success(function(data, status, headers, config) {
                $scope.todoList = data;
            });
    }
]);