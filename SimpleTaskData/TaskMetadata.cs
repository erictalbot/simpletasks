
using System;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;

namespace SimpleTaskData
{
    // To use Attribute with DBFirst :
    // http://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.metadatatypeattribute.aspx

    [MetadataType(typeof(TaskMetaData))]
    public partial class Task
    {
        #region properties

        public double DueDateMilliSeconds 
        { 
            get 
            { 
                DateTime UnixEpoch = new DateTime(1969,12,31,0,0,0);
                return(DueDate - UnixEpoch).TotalMilliseconds;
            } 
        }

        #endregion

    }

    public class TaskMetaData
    {
        [Required(ErrorMessage = "Descrition is required.")]
        public object Description;
    }

}
