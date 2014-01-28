using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.ComponentModel.DataAnnotations;        // This enables us to add DataType attribute to properties.
using SimpleTaskData;

namespace SimpleTaskApp.Models.Views
{
    public class TaskFormViewModel
    {

        #region Routing properties

        /// <summary>
        /// You store in VerbName a string specifying the method that you want to call in a specific controller
        /// with this instance of TaskFormViewModel. ( ex. Create, Edit, Delete ... )
        /// </summary>
        public string VerbName
        {
            get;
            set;
        }

        /// <summary>
        /// This is the controller the form post VERBNNAME (declared above) will be sent.
        /// </summary>
        public string ControllerName
        {
            get;
            set;
        }

        #endregion

        #region properties

        public string Description { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")] // http://msdn.microsoft.com/en-us/library/az4se3k1.aspx
        private DateTime _dueDate;

        public string DueDate { 
            get { 
                return _dueDate.ToString("dd-MM-yyyy"); 
            } 
            set {
                // fr-FR = DD-MM-YYYY
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                _dueDate = Convert.ToDateTime(value);   
            } 
        }

        public int SimpleTaskId { get; set; }

        public int Completion { get; set; }

        /// <summary>
        /// Simply build a Task from TaskFormViewModel
        /// </summary>
        public Task Task { 
            get { 
                return new Task() { 
                    Description = this.Description,
                    DueDate = _dueDate,
                    SimpleTaskId = SimpleTaskId
                }; 
            } 
        }

        #endregion

        #region constructors

        public TaskFormViewModel(Task task)
        {
            if (task != null)
            {
                Description = task.Description;
                SimpleTaskId = task.SimpleTaskId;
                // Here DueDate is in format MM/DD/YYYY which causes issues with the web page. Format used on the web is DD/MM/YYYY
                _dueDate = task.DueDate;
            }
        }

        public TaskFormViewModel() { }

        #endregion

    }
}