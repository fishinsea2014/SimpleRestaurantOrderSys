﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    public partial class DishTypeInfo
    {
        public DishTypeInfo() { }
        #region Model
        private int _did;
        private string _dtitle;
        private bool _disdelete;
        /// <summary>
		/// 
		/// </summary>
		public int DId
        {
            set { _did = value; }
            get { return _did; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DTitle
        {
            set { _dtitle = value; }
            get { return _dtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DIsDelete
        {
            set { _disdelete = value; }
            get { return _disdelete; }
        }
        #endregion Model
    }
}
