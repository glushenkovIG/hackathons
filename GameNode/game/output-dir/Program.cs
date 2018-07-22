using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Expload.Program {
    [System.Serializable]
    class IntResult {
       public int value;
       public string tpe;

       public static IntResult FromJson(string json) {
           return JsonUtility.FromJson<IntResult>(json);
       }
    }

    abstract class ProgramRequest<T>
    {
        public byte[] ProgramAddress { get; protected set; }

        public T Result { get; protected set; }
        public string Error { get; protected set; }
        public bool IsError { get; protected set; }

        protected ProgramRequest(byte[] programAddress)
        {
            ProgramAddress = programAddress;
            IsError = false;
            Error = "";
        }

        protected abstract T ParseResult(string json);

        protected IEnumerator SendJson(string json)
        {
            UnityWebRequest www = UnityWebRequest.Put("localhost:8087/api/program/method", json);
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                IsError = true;
                Error = www.error;
            }
            else
            {
                try
                {
                    Result = ParseResult(www.downloadHandler.text);
                }
                catch (ArgumentException e)
                {
                    IsError = true;
                    Error = "Invalid JSON: " + www.downloadHandler.text + "\n" + e.Message;
                }
            }
        }
    }

    class FightRequest: ProgramRequest<object> {

        public FightRequest(byte[] programAddress) : base(programAddress) { }

        protected override object ParseResult(string json)
        {
            return null;
        }

        public IEnumerator Fight(string arg0, string arg1)
        {
            String json = String.Format("{{ \"address\": {0}, \"method\": \"Fight\", \"args\": [{{ \"value\": {1}, \"tpe\": \"utf8\" }}, {{ \"value\": {2}, \"tpe\": \"utf8\" }}] }}",  "\"" + BitConverter.ToString(ProgramAddress).Replace("-","") + "\"" ,  "\"" + arg0" + "\"" ,  "\"" + arg1" + "\"" );
            yield return SendJson(json);
        }
    }
    class ShowWinsRequest: ProgramRequest<int> {

        public ShowWinsRequest(byte[] programAddress) : base(programAddress) { }

        protected override int ParseResult(string json)
        {
            return IntResult.FromJson(json).value;
        }

        public IEnumerator ShowWins(string arg0)
        {
            String json = String.Format("{{ \"address\": {0}, \"method\": \"ShowWins\", \"args\": [{{ \"value\": {1}, \"tpe\": \"utf8\" }}] }}",  "\"" + BitConverter.ToString(ProgramAddress).Replace("-","") + "\"" ,  "\"" + arg0" + "\"" );
            yield return SendJson(json);
        }
    }
    class ShowLosesRequest: ProgramRequest<int> {

        public ShowLosesRequest(byte[] programAddress) : base(programAddress) { }

        protected override int ParseResult(string json)
        {
            return IntResult.FromJson(json).value;
        }

        public IEnumerator ShowLoses(string arg0)
        {
            String json = String.Format("{{ \"address\": {0}, \"method\": \"ShowLoses\", \"args\": [{{ \"value\": {1}, \"tpe\": \"utf8\" }}] }}",  "\"" + BitConverter.ToString(ProgramAddress).Replace("-","") + "\"" ,  "\"" + arg0" + "\"" );
            yield return SendJson(json);
        }
    }
}