using ClassLibrary.Extensions;
using ClassLibrary.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Handlers
{
    public class UploadFilesHandler : IDisposable
    {
        #region variables
        private HttpClient HttpClient;
        private IJSRuntime JSRuntime;

        int MaxAllowedFiles;
        int MaxAllowedSize;
        
        /// <summary>
        /// All files uploaded
        /// </summary>
        private SortedDictionary<int, FileUploadContent> UploadedFiles = new SortedDictionary<int, FileUploadContent>();
        #endregion

        #region constructor
        /// <summary>
        /// Can upload files
        /// </summary>
        /// <param name="services"></param>
        /// <param name="maxFiles">number maximum of the files will send. Default 5</param>
        public UploadFilesHandler(IDefaultServices services = null, HttpClient httpClient = null, IJSRuntime jSRuntime = null, int maxFiles = 5, int maxSize = 512000)
        {
            if (services is not null)
            {
                HttpClient = services.Client ?? httpClient;
                JSRuntime = services.JsRuntime ?? jSRuntime;
            }
            if (httpClient is not null) HttpClient = httpClient;
            if (jSRuntime is not null) JSRuntime = jSRuntime;

            MaxAllowedFiles = maxFiles;
            MaxAllowedSize = maxSize;
        }
        #endregion

        #region properties
        // Define the indexer to allow client code to use [] notation to access directly to the file.
        public FileUploadContent this[int i] => UploadedFiles[i];

        /// <summary>
        /// Return first image from the dictionary
        /// </summary>
        public FileUploadContent First => UploadedFiles[UploadedFiles.Count - 1];

        /// <summary>
        /// Return last image from the dictionary
        /// </summary>
        public FileUploadContent Last => UploadedFiles[UploadedFiles.Count - 1];

        /// <summary>
        /// Return how many images have stored
        /// </summary>
        public int Count => UploadedFiles.Count;


        /// <summary>
        /// Return total file size uploaded
        /// </summary>
        public long Size => UploadedFiles.Sum(s => s.Value.Size);
        #endregion

        #region fields
        /// <summary>
        /// Last File stream uploaded
        /// </summary>
        public StreamContent UploadedImage;
        /// <summary>
        /// Last File name uploaded
        /// </summary>
        public string FileName;
        #endregion

        #region events
        public delegate void UploadEventHandler(object sender, FileUploadEventArgs e);
        public delegate void UploadsEventHandler(object sender, FilesUploadEventArgs e);
        public delegate void UploadErrorEventHandler(object sender, ArgumentException e);

        /// <summary>
        /// Event to notify each file uploaded
        /// </summary>
        public event UploadEventHandler OnUploadImage;

        /// <summary>
        /// Event to notify errors occurs
        /// </summary>
        public event UploadsEventHandler OnUploaded;

        /// <summary>
        /// Event to notify each file uploaded
        /// </summary>
        public event UploadErrorEventHandler OnUploadError;
        #endregion

        #region methods to manage files
        /// <summary>
        /// Use with InputFile OnChange
        /// </summary>
        /// <param name="e">InputFileChangeEventArgs</param>
        public void UploadImage(InputFileChangeEventArgs e)
        {
            try
            {
                if (e.FileCount == 0)
                {
                    UploadedImage = null;
                    FileName = null;
                    if (OnUploadError is not null)
                    {
                        OnUploadError(this, new ArgumentException("No images found", "UploadImage"));
                    }
                }
                else
                {
                    if (e.FileCount > this.MaxAllowedFiles)
                    {
                        if (OnUploadError is not null)
                        {
                            OnUploadError(this, new ArgumentException($"Max files can be selected is {this.MaxAllowedFiles}", "UploadImage"));
                        }
                    }
                    else
                    {
                        if (this.MaxAllowedFiles == 1)          //if only allowed 1 file always reset the dictionary
                            UploadedFiles = new SortedDictionary<int, FileUploadContent>();

                        int files = UploadedFiles.Count - 1;
                        long size = 0;
                        foreach (IBrowserFile file in e.GetMultipleFiles(maximumFileCount: MaxAllowedFiles))
                        {
                            size += file.Size;
                            Add(new FileUploadContent
                            {
                                Name = file.Name,
                                LastModified = file.LastModified,
                                Size = file.Size,
                                ContentType = file.ContentType,
                                FileStreamContent = new StreamContent(file.OpenReadStream(maxAllowedSize: MaxAllowedSize))
                            });
                            files++;
                        }

                        if (OnUploaded is not null)
                        {
                            OnUploaded(this, new FilesUploadEventArgs { Files = UploadedFiles, Count = files, Size = size, Action = "Added" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"Exception: {ex.Message}", "UploadImage"));
                }
            }            
        }

        #region object CRUD
        /// <summary>
        /// Add a image
        /// </summary>
        /// <param name="image"></param>
        public void Add(FileUploadContent image)
        {
            try
            {
                if (image.Size < this.MaxAllowedSize)
                {
                    if (this.MaxAllowedFiles == 1)          //if only allowed 1 file always reset the dictionary
                        UploadedFiles = new SortedDictionary<int, FileUploadContent>();

                    int index = UploadedFiles.Count;

                    if (index < this.MaxAllowedFiles)
                    {
                        //last image added is the default image to send
                        UploadedImage = image.FileStreamContent;
                        FileName = image.Name;

                        UploadedFiles.Add(index, image);
                        if (OnUploadImage is not null)
                        {
                            OnUploadImage(this, new FileUploadEventArgs { File = image, FileIndex = index, Action = "Added" });
                        }
                    }
                    else
                    {
                        if (OnUploadError is not null)
                        {
                            OnUploadError(this, new ArgumentException($"Max files is {this.MaxAllowedFiles}", "Add"));
                        }
                    }
                }
                else
                {
                    if (OnUploadError is not null)
                    {
                        OnUploadError(this, new ArgumentException($"File {image.Name} overload {this.MaxAllowedSize}", "Add"));
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"Exception: {ex.Message}", "Add"));
                }
            }
            
        }

        /// <summary>
        /// UJpdate image by index
        /// </summary>
        /// <param name="image"></param>
        public void Update(int index, FileUploadContent image)
        {
            if (OnUploadImage is not null)
            {
                OnUploadImage(this, new FileUploadEventArgs { File = this[index], FileIndex = index, Action = "Updating" });
            }
            UploadedFiles[index] = image;
            if (OnUploadImage is not null)
            {
                OnUploadImage(this, new FileUploadEventArgs { File = image, FileIndex = index, Action = "Updated" });
            }
        }

        /// <summary>
        /// Update image by file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="image"></param>
        public bool Update(string fileName, FileUploadContent image)
        {
            bool result;
            try
            {
                var file = UploadedFiles.First(i => i.Value.Name == fileName);
                if (file.Value is null)
                {
                    if (OnUploadError is not null)
                    {
                        OnUploadError(this, new ArgumentException($"Image {fileName} not found", "UpdateImage"));
                    }
                    result = false;
                }
                else
                {
                    Update(file.Key, image);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"Exception: {ex.Message}", "Update"));
                }
            }
            return result;
        }

        /// <summary>
        /// Remove image from index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Remove(int index)
        {
            FileUploadContent selection = UploadedFiles[index];
            bool result = UploadedFiles.Remove(index);
            if (result)
            {
                if (OnUploadImage is not null)
                {
                    OnUploadImage(this, new FileUploadEventArgs { File = selection, FileIndex = index, Action = "Removed" });
                }
            }
            else
            {
                if (OnUploadImage is not null)
                {
                    OnUploadImage(this, new FileUploadEventArgs { File = selection, FileIndex = index, Action = "Remove failed" });
                }
            }
            return result;
        }

        /// <summary>
        /// Remove image from file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Remove(string fileName)
        {
            bool result;
            try
            {
                var file = UploadedFiles.First(i => i.Value.Name == fileName);
                if (file.Value is null)
                {
                    if (OnUploadError is not null)
                    {
                        OnUploadError(this, new ArgumentException($"Image {fileName} not found", "RemoveImage"));
                    }
                    result = false;
                }
                else
                {
                    result = Remove(file.Key);
                }
            }
            catch (Exception ex)
            {
                result = false;
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"Exception: {ex.Message}", "Remove"));
                }
            }
            return result;

        }
        #endregion
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

        public void SetHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public void SetIJSRuntime(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }
        #endregion

        #region api calls
        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAsync<TModel>(string urlEndPoint, string field = "files", bool ignoreFiles = true)
        {
            if (UploadedImage is not null)
            {
                using MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(
                    content: UploadedImage,
                    name: field,
                    fileName: FileName
                );
                return await UploadImagesAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
            }
            else
            {
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"No files to upload", "UploadImageAsync"));
                }
                return default(TModel);
            }
        }
        
        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAsync<TModel>(string urlEndPoint, InputFileChangeEventArgs files, string field = "files", bool ignoreFiles = false) =>
            await UploadImageAsync<TModel>(urlEndPoint, new MultipartFormDataContent(), files, field, ignoreFiles);

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, string field = "files", bool ignoreFiles = false)
        {
            if (UploadedImage is not null)
            {
                content.Add(
                    content: UploadedImage,
                    name: field,
                    fileName: FileName
                );
            }
            return await UploadImagesAsync<TModel>(urlEndPoint, content, field, ignoreFiles);            
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, InputFileChangeEventArgs files, string field = "files", bool ignoreFiles = false)
        {
            UploadImage(files);
            return await UploadImagesAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAsync<TModel>(string urlEndPoint, MultipartFormDataContent content,  StreamContent file, string fileName = "", string field = "files", bool ignoreFiles = true)
        {
            if (file is not null)
            {
                content.Add(
                    content: file,
                    name: field,
                    fileName: string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() : fileName
                );
            }
            else
            {
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"No files to upload", "UploadImageAsync"));
                }
            }
            return await UploadImagesAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
        }

        /// <summary>
        /// Upload all files uploaded
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private async Task<TModel> UploadImagesAsync<TModel>(string urlEndPoint, MultipartFormDataContent content,
            string field = "files", bool ignoreFiles = false)
        {
            if (HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
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
                if (OnUploaded is not null)
                {
                    OnUploaded(this, new FilesUploadEventArgs { Count = c, Files = UploadedFiles, Size = size });
                }
            }

            using HttpResponseMessage result = await HttpClient.PostAsync(urlEndPoint, content);
            TModel response = await result.Content.ReadFromJsonAsync<TModel>();
            return response;
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="endPoint">Must be return boolean the endpoint</param>
        /// <param name="index"></param>
        /// <param name="field">name the endpoint are expecting to send the file name</param>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string endPoint, int index, string field) =>
            await DeleteImageAsync(endPoint, this[index].Name, field);

        /// <summary>
        /// Delete the image from the filename
        /// </summary>
        /// <param name="endPoint">Must be return boolean the endpoint</param>
        /// <param name="filename"></param>
        /// <param name="field">name the endpoint are expecting to send the file name</param>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string endPoint, string filename, string field)
        {
            if (HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
            if (string.IsNullOrEmpty(filename)) return false;

            using MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(filename), field);

            using HttpResponseMessage response = await HttpClient.PostAsync(endPoint, content);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion

        #region api call with auth
        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAuthAsync<TModel>(string urlEndPoint, string field = "files", bool ignoreFiles = true)
        {
            if (UploadedImage is not null)
            {
                using MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(
                    content: UploadedImage,
                    name: field,
                    fileName: FileName
                );
                return await UploadImagesAuthAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
            }
            else
            {
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"No files to upload", "UploadImageAsync"));
                }
                return default(TModel);
            }
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAuthAsync<TModel>(string urlEndPoint, InputFileChangeEventArgs files, string field = "files", bool ignoreFiles = false) =>
            await UploadImageAuthAsync<TModel>(urlEndPoint, new MultipartFormDataContent(), files, field, ignoreFiles);

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, string field = "files", bool ignoreFiles = false)
        {
            if (UploadedImage is not null)
            {
                content.Add(
                    content: UploadedImage,
                    name: field,
                    fileName: FileName
                );
            }
            return await UploadImagesAuthAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="files"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, InputFileChangeEventArgs files, string field = "files", bool ignoreFiles = false)
        {
            UploadImage(files);
            return await UploadImagesAuthAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
        }

        /// <summary>
        /// Upload a image using the endpoint send
        /// </summary>
        /// <typeparam name="TModel">Model to use on the response from the url end point</typeparam>
        /// <param name="content">form content to send to the url end point</param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="urlEndPoint"></param>
        /// <returns></returns>
        public async Task<TModel> UploadImageAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content, StreamContent file, string fileName = "", string field = "files", bool ignoreFiles = true)
        {
            if (file is not null)
            {
                content.Add(
                    content: file,
                    name: field,
                    fileName: string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() : fileName
                );
            }
            else
            {
                if (OnUploadError is not null)
                {
                    OnUploadError(this, new ArgumentException($"No files to upload", "UploadImageAsync"));
                }
            }
            return await UploadImagesAuthAsync<TModel>(urlEndPoint, content, field, ignoreFiles);
        }

        /// <summary>
        /// Upload all files uploaded
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="urlEndPoint"></param>
        /// <param name="content"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private async Task<TModel> UploadImagesAuthAsync<TModel>(string urlEndPoint, MultipartFormDataContent content,
            string field = "files", bool ignoreFiles = false)
        {
            if (HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
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
                if (OnUploaded is not null)
                {
                    OnUploaded(this, new FilesUploadEventArgs { Count = c, Files = UploadedFiles, Size = size });
                }
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
        /// <param name="field">name the endpoint are expecting to send the file name</param>
        /// <returns></returns>
        public async Task<bool> DeleteImageAuthAsync(string endPoint, int index, string field) =>
            await DeleteImageAuthAsync(endPoint, this[index].Name, field);

        /// <summary>
        /// Delete the image from the filename
        /// </summary>
        /// <param name="endPoint">Must be return boolean the endpoint</param>
        /// <param name="filename"></param>
        /// <param name="field">name the endpoint are expecting to send the file name</param>
        /// <returns></returns>
        public async Task<bool> DeleteImageAuthAsync(string endPoint, string filename, string field)
        {
            if (HttpClient is null) throw new ArgumentException("At least HttpClient Must be provided. Use HttpClient or IDefaultServices.");
            if (JSRuntime is null) throw new ArgumentException("At least IJSRuntime Must be provided. Use IJSRuntime or IDefaultServices.");
            if (string.IsNullOrEmpty(filename)) return false;

            using MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(filename), field);

            using HttpResponseMessage response = await HttpClient.PostAuthAsync(JSRuntime, endPoint, content);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion

        #region dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UploadedImage.Dispose();
                    int c = UploadedFiles.Count;
                    for (int i = 0; i < c; i++)
                    {
                        UploadedFiles[i].FileStreamContent.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    /// <summary>
    /// Return file name and file stream per each file uploaded
    /// </summary>
    public class FileUploadEventArgs : EventArgs
    {
        public FileUploadContent File { get; set; }
        public int FileIndex { get; set; }
        public string Action { get; set; }
    }

    /// <summary>
    /// Return all files uploaded
    /// </summary>
    public class FilesUploadEventArgs : EventArgs
    {
        public SortedDictionary<int, FileUploadContent> Files { get; set; }
        public long Size { get; set; }
        public int Count { get; set; }
        public string Action { get; set; }
    }


    /// <summary>
    /// Manage the file upload
    /// </summary>
    public class FileUploadContent
    {
        //
        // Summary:
        //     The name of the file as specified by the browser.
        public string Name { get; set; }
        //
        // Summary:
        //     The last modified date as specified by the browser.
        public DateTimeOffset LastModified { get; set; }
        //
        // Summary:
        //     The size of the file in bytes as specified by the browser.
        public long Size { get; set; }
        //
        // Summary:
        //     The MIME type of the file as specified by the browser.
        public string ContentType { get; set; }

        public StreamContent FileStreamContent { get; set; }
    }

}
