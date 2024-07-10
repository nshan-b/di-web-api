namespace di_web_api.Models {
    public class DocumentUpdateRequest {
        public string? AgencyAddress_Content { get; set; }
        public string? AgencyCounty_Content { get; set; }
        public string? AgencyNU_Content { get; set; }
        public string? AgencyName_Content { get; set; }

        public string? BM1_Address_Content { get; set; }
        public string? BM1_Name_Content { get; set; }

        public string? BM2_Address_Content { get; set; }
        public string? BM2_Name_Content { get; set; }

        public string? BM3_Address_Content { get; set; }
        public string? BM3_Name_Content { get; set; }

        public string? BM4_Address_Content { get; set; }
        public string? BM4_Name_Content { get; set; }

        public string? BM5_Address_Content { get; set; }
        public string? BM5_Name_Content { get; set; }

        public string? ClerkAddress_Content { get; set; }
        public string? ClerkName_Content { get; set; }
        public string? ClerkTitle_Content { get; set; }

        public bool? Initial_Content { get; set; }

        public string? PresidentAddress_Content { get; set; }
        public string? PresidentName_Content { get; set; }
        public string? PresidentTitle_Content { get; set; }

        public DateTime? SignedDate_Content { get; set; }

        public string? SignedName_Content { get; set; }

        public bool? Updated_Content { get; set; }
        public bool? HasSignature { get; set; }
    }

}
