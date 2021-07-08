using BlazorInputFileExtended;
using ClassLibrary.Extensions;
using ClassLibrary.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Handlers
{
    public class UploadFilesHandler : InputFileHandler
    {
        #region variables
        private IJSRuntime JSRuntime;
        #endregion

        #region constructor
        /// <summary>
        /// Can upload files
        /// </summary>
        /// <param name="services"></param>
        /// <param name="httpClient"></param>
        /// <param name="jSRuntime"></param>
        /// <param name="maxFiles">Maximum files allowed to upload</param>
        /// <param name="maxSize">Maximum file size to upload</param>
        public UploadFilesHandler(IDefaultServices services = null, HttpClient httpClient = null, IJSRuntime jSRuntime = null, int maxFiles = 5, long maxSize = 512000) : 
            base(httpClient, maxFiles, maxSize)
        {
            if (services is not null)
            {
                HttpClient = services.Client ?? httpClient;
                JSRuntime = services.JsRuntime ?? jSRuntime;
            }
            if (jSRuntime is not null) JSRuntime = jSRuntime;

        }
        #endregion

        #region methods for api calls
        /// <summary>
        /// Get IDefaultServices to set HttpClient and JSRuntime
        /// </summary>
        /// <param name="services"></param>
        public void SetService(IDefaultServices services)
        {
            HttpClient = services.Client;
            JSRuntime = services.JsRuntime;
        }

        /// <summary>
        /// Set IJSRuntime if it's not from the constructor
        /// </summary>
        /// <param name="jSRuntime"></param>
        public void SetIJSRuntime(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }
        #endregion

        #region api call with auth
        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="ignoreFiles">Indicate if need to ignore the files or not</param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, bool ignoreFiles = true, string field = "files") =>
            await UploadAuthAsync<TModel>(urlEndPoint, new MultipartFormDataContent(), true);


        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="files"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, InputFileChangeEventArgs files, string field = "files") =>
            await UploadAuthAsync<TModel>(urlEndPoint, new MultipartFormDataContent(), files, field);

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content">form content to send to the url end point</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content) =>
            await UploadAuthAsync<TModel>(urlEndPoint, content, true);

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="ignoreFiles">Indicate if need to ignore the files or not</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, bool ignoreFiles, string field = "files")
        {
            if (ignoreFiles)
            {
                if (UploadedImage is not null)
                {
                    content.Add(
                        content: UploadedImage,
                        name: field,
                        fileName: FileName
                    );
                }
            }
            return await UploadFilesAuthAsync<TModel>(urlEndPoint, content, ignoreFiles);
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="files"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, InputFileChangeEventArgs files, string field = "files")
        {
            UploadFile(files);
            return await UploadFilesAuthAsync<TModel>(urlEndPoint, content, false, field);
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<TModel> UploadAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, StreamContent file, string fileName = "", string field = "files", bool ignoreFiles = true)
        {
            if (file is not null)
            {
                content.Add(
                    content: file,
                    name: field,
                    fileName: string.IsNullOrEmpty(fileName) ? FileName : fileName
                );
            }
            else
            {
                OnUploadErrorEvent(this, new ArgumentException($"No files to upload", "UploadImageAsync"));            
            }
            return await UploadFilesAuthAsync<TModel>(urlEndPoint, content, ignoreFiles, field);
        }

        /// <summary>
        /// Upload all files uploaded
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        private async Task<TModel> UploadFilesAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content,
            bool ignoreFiles, string field = "files")
        {
            if (this.HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
            if (JSRuntime is null) throw new ArgumentException("At least IJSRuntime Must be provided. Use IJSRuntime or IDefaultServices.");
            if (!ignoreFiles)
            {
                int c = UploadedFiles.Count;
                long size = 0;
                for (int i = 0; i < c; i++)
                {
                    content.Add(
                        content: UploadedFiles[i].FileStreamContent,
                        name: field,
                        fileName: UploadedFiles[i].Name
                    );
                    size += UploadedFiles[i].Size;
                }
                OnUploadedEvent(this, new FilesUploadEventArgs { Count = c, Files = UploadedFiles, Size = size });              
            }

            using HttpResponseMessage result = await HttpClient.PostAuthAsync(JSRuntime, urlEndPoint, content);
            TModel response = await result.Content.ReadFromJsonAsync<TModel>();
            return response;
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="endPoint">Must be return boolean the endpoint</param>
        /// <param name="index"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthAsync(string endPoint, int index, string field) =>
            await DeleteAuthAsync(endPoint, this[index].Name, field);

        /// <summary>
        /// Delete the image from the filename
        /// </summary>
        /// <param name="endPoint">Must be return boolean the endpoint</param>
        /// <param name="filename"></param>
        /// <param name="field">form content name to upload the file</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthAsync(string endPoint, string filename, string field)
        {
            if (HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
            if (JSRuntime is null) throw new ArgumentException("At least IJSRuntime Must be provided. Use IJSRuntime or IDefaultServices.");
            if (string.IsNullOrEmpty(filename)) return false;

            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(filename), field);

            using HttpResponseMessage response = await HttpClient.PostAuthAsync(JSRuntime, endPoint, content);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion

        #region dispose
        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UploadedImage?.Dispose();
                    int c = UploadedFiles.Count;
                    for (int i = 0; i < c; i++)
                    {
                        UploadedFiles[i]?.FileStreamContent?.Dispose();
                    }
                }
                disposedValue = true;
                base.Dispose(disposing);
            }
        }
        #endregion
    }

}
