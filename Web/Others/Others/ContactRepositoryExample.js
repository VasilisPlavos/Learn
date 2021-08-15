var Module = (function (){
    return {

        Method: function  (a) {
            console.log(a);
        }

    }})();

Module.Method(6);


var _contactRepo = (function(){
    return {


        GetContactById: function(id)
        {
            if (id != 1) {return 'NotFound'}
            return 'Vasilis'
        }
    }
})()


var name = _contactRepo.GetContactById(1);
console.log(name);