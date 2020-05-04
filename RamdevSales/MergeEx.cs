using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RamdevSales
{
    public class MergeEx
    {
        #region Fields
        private string sourcefolder;
        private string destinationfile;
        private IList fileList = new ArrayList();
        #endregion

        #region Public Methods
        ///
        /// Add a new file, together with a given docname to the fileList and namelist collection
        ///
        public void AddFile(string pathnname)
        {
            fileList.Add(pathnname);
        }

        ///
        /// Generate the merged PDF
        ///
        public void Execute()
        {
            MergeDocs();
        }

        private void MergeDocs()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        ///
        /// Merges the Docs and renders the destinationFile
        ///
    
    }
}
