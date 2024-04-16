using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AETOS.Scripts.API;
using ButtonsAPI.Enums;
using ButtonsAPI.Interfaces;
using ButtonsAPI.Models;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace ButtonsAPI.Services
{
    public class ButtonsApiService : IButtonsApi
    {
        private readonly string _baseUrl;

        public ButtonsApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void DeleteButton(int id)
        {
            DoRequest($"{_baseUrl}/{id}", RequestType.Delete);
        }

        public IButton EditButton(int id, ButtonRequest buttonData = null)
        {
            string result;
            if (buttonData != null)
            {
                string json = JsonConvert.SerializeObject(buttonData);
                result = DoRequest($"{_baseUrl}/{id}", RequestType.Put, json);
            }
            else
            {
                result = DoRequest($"{_baseUrl}/{id}", RequestType.Put);
            }

            ButtonResponse responseData = JsonConvert.DeserializeObject<ButtonResponse>(result);
            return responseData;
        }

        public IButton AddButton(ButtonRequest buttonData = null)
        {
            string result;
            if (buttonData != null)
            {
                string json = JsonConvert.SerializeObject(buttonData);
                result = DoRequest(_baseUrl, RequestType.Post, json);
            }
            else
            {
                result = DoRequest(_baseUrl, RequestType.Post);
            }

            ButtonResponse responseData = JsonConvert.DeserializeObject<ButtonResponse>(result);
            return responseData;
        }

        public List<IButton> GetAllButtons()
        {
            string result = DoRequest(_baseUrl, RequestType.Get);
            List<ButtonResponse> responseData = JsonConvert.DeserializeObject<List<ButtonResponse>>(result);
            return new List<IButton>(responseData);
        }

        private string DoRequest(string url, RequestType type, string json = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type.GetString();

                if (json != null)
                {
                    request.ContentType = "application/json";
                    using StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                if (dataStream == null)
                {
                    return string.Empty;
                }

                StreamReader reader = new StreamReader(dataStream);
                string result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return string.Empty;
            }
        }
    }
}