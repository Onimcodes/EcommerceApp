using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IdentityService.application.Common.Dtos
{
    public interface IRequestResponse
    {
        int ResponseCode { get; set; }
        string ResponseMessage { get; set; }
        object? ResponseData { get; set; }
        List<string>? Errors { get; set; }
    }
    public record RequestResponse<T> : IRequestResponse
    {
        [JsonPropertyName("responseCode")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public string? ResponseMessage { get; set; }

        [JsonPropertyName("responseData")]
        public T? ResponseData { get; set; }
        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        // Explicit implementation for IRequestResponse's ResponseData property
        object? IRequestResponse.ResponseData
        {
            get => ResponseData;
            set => ResponseData = (T?)value;
        }
    }
}
