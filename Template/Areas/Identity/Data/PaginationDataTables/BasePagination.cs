using System;
namespace Template.Areas.Identity.Data.PaginationDataTables
{
	public class BasePagination
	{
        // <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string Echo { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int DisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int DisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int SortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Order no of the column that is used to do sorting
        /// </summary>
        public int SortCol_0 { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary>
        public string SortDir_0 { get; set; }
    }
}

