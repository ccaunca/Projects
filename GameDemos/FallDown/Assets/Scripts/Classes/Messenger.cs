using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallDownDemo
{
    public enum MessengerTypeEnum
    {
        Web
    }
    public class Messenger : MonoBehaviour
    {
        private MessengerTypeEnum _messengerType;
        private Message _message;
        public Message Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    _message = value;
                }
            }
        }
        private string _destination;    // maybe create a Destination class in the future?
        public string Destination
        {
            get { return _destination; }
            set
            {
                if (value != _destination)
                {
                    _destination = value;
                }
            }
        }
        public Messenger(string destination)
        {
            _messengerType = MessengerTypeEnum.Web; // by default, can extend in the future
            _destination = destination;
        }

        public bool SendMessage(Message message)
        {
            this._message = message;
            WWWForm requestForm = new WWWForm();
            foreach(Field field in message.Fields)
            {
                requestForm.AddField(field.Name, field.Value);
            }
            WWW request = new WWW(this.Destination, requestForm);
            StartCoroutine(WaitForRequest(request));
            return request.error == null;
        }
        IEnumerator WaitForRequest(WWW request)
        {
            yield return request;
            if (request.error == null)
            {
                ReceiveResponse(request.text);
            }
            else
            {
                Debug.Log("WWW Error: " + request.error);
            }
        }
        private void ReceiveResponse(string response)
        {
            Debug.Log(response);
        }
    }
}
