using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Models
{
    public abstract class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>(); 
        public bool ShowMessages { get; set; } 
        public bool IsValid => !Messages.Exists(x => x.MessageType > MessageType.Success);
        public bool InError => Messages.Exists(x => x.MessageType >= MessageType.Error);
        public bool HasAnyMessages => Messages.Any();
        public bool HasInformationMessages => Messages.Exists(x => x.MessageType == MessageType.Information);
        public bool HasSuccessMessages => Messages.Exists(x => x.MessageType == MessageType.Success);
        public bool HasWarningMessages => Messages.Exists(x => x.MessageType == MessageType.Warning);
        public bool HasValidationMessages => Messages.Exists(x => x.MessageType == MessageType.Validation);
        public bool HasCautionMessages => Messages.Exists(x => x.MessageType == MessageType.Caution);
        public bool HasErrorMessages => Messages.Exists(x => x.MessageType == MessageType.Error);
        public bool HasExceptionMessages => Messages.Exists(x => x.MessageType == MessageType.Exception);
        public bool HasFatalErrorExceptionMessages => Messages.Exists(x => x.MessageType == MessageType.FatalErrorException);

        public BaseResponse()
        {
            Messages = new List<Message>();
            ShowMessages = true;
        }
        public virtual void AddMessage(string code, string text, string? title, MessageType type, bool showMessage = true)
        {
            this.Messages.Add(new Message()
            {
                Code = code,
                Title = title,
                Text = text,
                MessageType = type,
                ShowMessage = true
            });
        }

        public virtual void AddInformationMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Information, showMessage);
        }

        public virtual void AddSuccessMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Success, showMessage);
        }

        public virtual void AddWarningMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Warning, showMessage);
        }

        public virtual void AddValidationMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Validation, showMessage);
        }

        public virtual void AddCautionMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Caution, showMessage);
        }

        public virtual void AddErrorMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Error, showMessage);
        }

        public virtual void AddExceptionMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.Exception, showMessage);
        }

        public virtual void AddFatalErrorExceptionMessage(string code, string text, string? title = null, bool showMessage = true)
        {
            AddMessage(code, text, title, MessageType.FatalErrorException, showMessage);
        }

        public void Merge(BaseResponse response)
        {
            this.Messages.AddRange(response.Messages);
        }
    }
}
