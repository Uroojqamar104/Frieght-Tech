using System.Collections.Generic;

namespace FreightTech.Data.DataTransferObject
{
    public class ResponseModel//<T> where T : class
    {
        public ResponseModel()
        {
            errors = new List<string>();
            message = string.Empty;
        }

        public bool status { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
        public object response { get; set; }

        //public void SetRequestObject<T>(T value) where T : class
        //{
        //}

        //public T GetRequestObject<T>() where T : class
        //{
        //}
    }

    //public class ResponseModel1<T>
    //{
    //    private T _value;

    //    public T Value {
    //        get {
    //            // insert desired logic here
    //            return _value;
    //        }
    //        set {
    //            // insert desired logic here
    //            _value = value;
    //        }
    //    }

    //    public static implicit operator T(ResponseModel1<T> value)
    //    {
    //        return value.Value;
    //    }

    //    public static implicit operator ResponseModel1<T>(T value)
    //    {
    //        return new ResponseModel1<T> { Value = value };
    //    }
    //}
}
