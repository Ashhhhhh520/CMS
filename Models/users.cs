﻿using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace CMS.Models {

	[JsonObject(MemberSerialization.OptIn), Table(DisableSyncStructure = true)]
	public partial class users {

		[JsonProperty, Column(DbType = "int", IsPrimary = true)]
		public int ID { get; set; }

		[JsonProperty, Column(DbType = "datetime")]
		public DateTime? AddDate { get; set; }

		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public string Password { get; set; }

		[JsonProperty, Column(DbType = "int")]
		public int? Sex { get; set; }

		[JsonProperty]
		public string UserName { get; set; }

	}

}
