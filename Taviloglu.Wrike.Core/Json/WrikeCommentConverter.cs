﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Taviloglu.Wrike.Core.Comments;

namespace Taviloglu.Wrike.Core.Json
{
    internal class WrikeCommentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WrikeComment);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            JToken token;

            bool isFolderComment = jObject.TryGetValue("folderId", out token) && token.Type == JTokenType.String;
            if (isFolderComment)
            {
                return jObject.ToObject<WrikeFolderComment>();
            }
            else
            {
                return jObject.ToObject<WrikeTaskComment>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
