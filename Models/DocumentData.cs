namespace di_web_api.Models {
    public class DocumentData {
        public int Id { get; set; } // Assuming Id is your primary key and is not nullable

        // File content
        public byte[]? FileContent { get; set; }
        public string? FileType { get; set; } // Add this property to store the file type


        // Agency fields
        public string? AgencyAddress_Content { get; set; }
        public double? AgencyAddress_Confidence { get; set; } // Nullable double

        public string? AgencyCounty_Content { get; set; }
        public double? AgencyCounty_Confidence { get; set; } // Nullable double

        public string? AgencyNU_Content { get; set; }
        public double? AgencyNU_Confidence { get; set; } // Nullable double

        public string? AgencyName_Content { get; set; }
        public double? AgencyName_Confidence { get; set; } // Nullable double

        // Board Member fields
        public string? BM1_Address_Content { get; set; }
        public double? BM1_Address_Confidence { get; set; } // Nullable double

        public string? BM1_Name_Content { get; set; }
        public double? BM1_Name_Confidence { get; set; } // Nullable double

        public string? BM2_Address_Content { get; set; }
        public double? BM2_Address_Confidence { get; set; } // Nullable double

        public string? BM2_Name_Content { get; set; }
        public double? BM2_Name_Confidence { get; set; } // Nullable double

        public string? BM3_Address_Content { get; set; }
        public double? BM3_Address_Confidence { get; set; } // Nullable double

        public string? BM3_Name_Content { get; set; }
        public double? BM3_Name_Confidence { get; set; } // Nullable double

        public string? BM4_Address_Content { get; set; }
        public double? BM4_Address_Confidence { get; set; } // Nullable double

        public string? BM4_Name_Content { get; set; }
        public double? BM4_Name_Confidence { get; set; } // Nullable double

        public string? BM5_Address_Content { get; set; }
        public double? BM5_Address_Confidence { get; set; } // Nullable double

        public string? BM5_Name_Content { get; set; }
        public double? BM5_Name_Confidence { get; set; } // Nullable double

        // Clerk fields
        public string? ClerkAddress_Content { get; set; }
        public double? ClerkAddress_Confidence { get; set; } // Nullable double

        public string? ClerkName_Content { get; set; }
        public double? ClerkName_Confidence { get; set; } // Nullable double

        public string? ClerkTitle_Content { get; set; }
        public double? ClerkTitle_Confidence { get; set; } // Nullable double

        // President fields
        public string? PresidentName_Content { get; set; }
        public double? PresidentName_Confidence { get; set; } // Nullable double

        public string? PresidentAddress_Content { get; set; }
        public double? PresidentAddress_Confidence { get; set; } // Nullable double
        public string? PresidentTitle_Content { get; set; }
        public double? PresidentTitle_Confidence { get; set; } // Nullable double


        // Boolean and Date fields
        public bool? Initial_Content { get; set; } // Nullable boolean
        public double? Initial_Confidence { get; set; } // Nullable double

        public DateTime? SignedDate_Content { get; set; } // Nullable string for date
        public double? SignedDate_Confidence { get; set; } // Nullable double

        public string? SignedName_Content { get; set; }
        public double? SignedName_Confidence { get; set; } // Nullable double

        public bool? Updated_Content { get; set; } // Nullable boolean
        public double? Updated_Confidence { get; set; } // Nullable double

        // Other fields as needed

   
    }
}
