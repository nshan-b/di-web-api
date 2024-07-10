using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Azure.AI.DocumentIntelligence;
using Azure;
using System.Text.Json;
using System.IO;
using di_web_api.Data;
using di_web_api.Models;

namespace di_web_api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DocController : Controller {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly DBContext _context;

        public DocController(IConfiguration configuration, DBContext context) {
            _configuration = configuration;
            _context = context;
        }




    [HttpGet("getDocument/{id}")]
        public async Task<IActionResult> GetDocumentById(int id) {
            try {
                var document = await _context.DocumentData
                    .Where(d => d.Id == id)
                    .Select(d => new {
                        d.Id,
                        d.AgencyAddress_Content,
                        d.AgencyAddress_Confidence,
                        d.AgencyCounty_Content,
                        d.AgencyCounty_Confidence,
                        d.AgencyNU_Content,
                        d.AgencyNU_Confidence,
                        d.AgencyName_Content,
                        d.AgencyName_Confidence,
                        d.BM1_Address_Content,
                        d.BM1_Address_Confidence,
                        d.BM1_Name_Content,
                        d.BM1_Name_Confidence,
                        d.BM2_Address_Content,
                        d.BM2_Address_Confidence,
                        d.BM2_Name_Content,
                        d.BM2_Name_Confidence,
                        d.BM3_Address_Content,
                        d.BM3_Address_Confidence,
                        d.BM3_Name_Content,
                        d.BM3_Name_Confidence,
                        d.BM4_Address_Content,
                        d.BM4_Address_Confidence,
                        d.BM4_Name_Content,
                        d.BM4_Name_Confidence,
                        d.BM5_Address_Content,
                        d.BM5_Address_Confidence,
                        d.BM5_Name_Content,
                        d.BM5_Name_Confidence,
                        d.ClerkAddress_Content,
                        d.ClerkAddress_Confidence,
                        d.ClerkName_Content,
                        d.ClerkName_Confidence,
                        d.ClerkTitle_Content,
                        d.ClerkTitle_Confidence,
                        d.Initial_Content,
                        d.Initial_Confidence,
                        d.PresidentAddress_Content,
                        d.PresidentAddress_Confidence,
                        d.PresidentName_Content,
                        d.PresidentName_Confidence,
                        d.PresidentTitle_Content,
                        d.PresidentTitle_Confidence,
                        d.SignedDate_Content,
                        d.SignedDate_Confidence,
                        d.SignedName_Content,
                        d.SignedName_Confidence,
                        d.Updated_Content,
                        d.Updated_Confidence,
                        Status = _context.DocumentStatus
                            .Where(s => s.DocumentId == d.Id)
                            .Select(s => new {
                                s.Status,
                                s.Created,
                                s.Updated,
                                s.Type
                            })
                            .FirstOrDefault()
                    })
                    .FirstOrDefaultAsync();

                if (document == null) {
                    return NotFound();
                }

                return Ok(document);
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("updateDocument/{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateRequest requestData) {
            try {
                // Retrieve the existing document entity from the database
                var document = await _context.DocumentData.FindAsync(id);
                var documentStatus = await _context.DocumentStatus.FirstOrDefaultAsync(ds => ds.DocumentId == id);


                if (document == null) {
                    return NotFound($"Document with id {id} not found.");
                }

                // Update Status
                documentStatus.Updated = DateTime.UtcNow;
                if (requestData.HasSignature == true) {
                    documentStatus.Status = "completed";
                }


                _context.Update(documentStatus);
                await _context.SaveChangesAsync();


                // Update fields based on DocumentUpdateRequest
                document.AgencyAddress_Content = requestData.AgencyAddress_Content ?? document.AgencyAddress_Content;
                document.AgencyCounty_Content = requestData.AgencyCounty_Content ?? document.AgencyCounty_Content;
                document.AgencyNU_Content = requestData.AgencyNU_Content ?? document.AgencyNU_Content;
                document.AgencyName_Content = requestData.AgencyName_Content ?? document.AgencyName_Content;

                document.BM1_Address_Content = requestData.BM1_Address_Content ?? document.BM1_Address_Content;
                document.BM1_Name_Content = requestData.BM1_Name_Content ?? document.BM1_Name_Content;

                document.BM2_Address_Content = requestData.BM2_Address_Content ?? document.BM2_Address_Content;
                document.BM2_Name_Content = requestData.BM2_Name_Content ?? document.BM2_Name_Content;

                document.BM3_Address_Content = requestData.BM3_Address_Content ?? document.BM3_Address_Content;
                document.BM3_Name_Content = requestData.BM3_Name_Content ?? document.BM3_Name_Content;

                document.BM4_Address_Content = requestData.BM4_Address_Content ?? document.BM4_Address_Content;
                document.BM4_Name_Content = requestData.BM4_Name_Content ?? document.BM4_Name_Content;

                document.BM5_Address_Content = requestData.BM5_Address_Content ?? document.BM5_Address_Content;
                document.BM5_Name_Content = requestData.BM5_Name_Content ?? document.BM5_Name_Content;

                document.ClerkAddress_Content = requestData.ClerkAddress_Content ?? document.ClerkAddress_Content;
                document.ClerkName_Content = requestData.ClerkName_Content ?? document.ClerkName_Content;
                document.ClerkTitle_Content = requestData.ClerkTitle_Content ?? document.ClerkTitle_Content;

                document.Initial_Content = requestData.Initial_Content ?? document.Initial_Content;

                document.PresidentAddress_Content = requestData.PresidentAddress_Content ?? document.PresidentAddress_Content;
                document.PresidentName_Content = requestData.PresidentName_Content ?? document.PresidentName_Content;
                document.PresidentTitle_Content = requestData.PresidentTitle_Content ?? document.PresidentTitle_Content;

                document.SignedDate_Content = requestData.SignedDate_Content ?? document.SignedDate_Content;

                document.SignedName_Content = requestData.SignedName_Content ?? document.SignedName_Content;

                document.Updated_Content = requestData.Updated_Content ?? document.Updated_Content;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok($"Document with id {id} successfully updated.");
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    

    [HttpGet("getDocuments")]
        public async Task<IActionResult> GetDocuments() {
            try {
                var documents = await _context.DocumentData.ToListAsync();

                var jsonResponse = documents.Select(doc => new {
                    doc.Id,
                    doc.AgencyAddress_Content,
                    doc.AgencyAddress_Confidence,
                    doc.AgencyCounty_Content,
                    doc.AgencyCounty_Confidence,
                    doc.AgencyNU_Content,
                    doc.AgencyNU_Confidence,
                    doc.AgencyName_Content,
                    doc.AgencyName_Confidence,
                    doc.BM1_Address_Content,
                    doc.BM1_Address_Confidence,
                    doc.BM1_Name_Content,
                    doc.BM1_Name_Confidence,
                    doc.BM2_Address_Content,
                    doc.BM2_Address_Confidence,
                    doc.BM2_Name_Content,
                    doc.BM2_Name_Confidence,
                    doc.BM3_Address_Content,
                    doc.BM3_Address_Confidence,
                    doc.BM3_Name_Content,
                    doc.BM3_Name_Confidence,
                    doc.BM4_Address_Content,
                    doc.BM4_Address_Confidence,
                    doc.BM4_Name_Content,
                    doc.BM4_Name_Confidence,
                    doc.BM5_Address_Content,
                    doc.BM5_Address_Confidence,
                    doc.BM5_Name_Content,
                    doc.BM5_Name_Confidence,
                    doc.ClerkAddress_Content,
                    doc.ClerkAddress_Confidence,
                    doc.ClerkName_Content,
                    doc.ClerkName_Confidence,
                    doc.ClerkTitle_Content,
                    doc.ClerkTitle_Confidence,
                    doc.Initial_Content,
                    doc.Initial_Confidence,
                    doc.PresidentAddress_Content,
                    doc.PresidentAddress_Confidence,
                    doc.PresidentName_Content,
                    doc.PresidentName_Confidence,
                    doc.PresidentTitle_Content,
                    doc.PresidentTitle_Confidence,
                    doc.SignedDate_Content,
                    doc.SignedDate_Confidence,
                    doc.SignedName_Content,
                    doc.SignedName_Confidence,
                    doc.Updated_Content,
                    doc.Updated_Confidence,
                    Status = _context.DocumentStatus
                            .Where(s => s.DocumentId == doc.Id)
                            .Select(s => new {
                                s.Status,
                                s.Created,
                                s.Type
                            })
                            .FirstOrDefault()
                });

                return Ok(jsonResponse);
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getDocumentFile/{id}")]
        public async Task<IActionResult> GetDocumentFile(int id) {
            try {
                var document = await _context.DocumentData.FindAsync(id);
                if (document == null || document.FileContent == null) {
                    return NotFound();
                }

                var fileType = document.FileType ?? "application/pdf";
                var fileName = fileType == "image/jpeg" ? "image.jpg" : "document.pdf";

                return File(document.FileContent, fileType, fileName); // Adjust MIME type and filename as needed
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("saveDocument")]
        public async Task<IActionResult> SaveDocument([FromForm] IFormFile file, [FromForm] string document) {
            try {
                Console.WriteLine("saveDocument");
                if (document == null) {
                    return BadRequest();
                }

                // Deserialize the document data from JSON
                var documentData = JsonSerializer.Deserialize<Document>(document);

                if (documentData == null) {
                    return BadRequest("Invalid document data.");
                }


                byte[] fileContent = null;
                string fileType = null;

                if (file != null) {
                    using (var memoryStream = new MemoryStream()) {
                        await file.CopyToAsync(memoryStream);
                        fileContent = memoryStream.ToArray();
                        fileType = file.ContentType;
                    }
                }

                var documentEntity = new DocumentData {
                    FileContent = fileContent,
                    FileType = fileType,
                    AgencyAddress_Content = documentData.AgencyAddress?.Content,
                    AgencyAddress_Confidence = documentData.AgencyAddress?.Confidence ?? 0,
                    AgencyCounty_Content = documentData.AgencyCounty?.Content,
                    AgencyCounty_Confidence = documentData.AgencyCounty?.Confidence ?? 0,
                    AgencyNU_Content = documentData.AgencyNU?.Content,
                    AgencyNU_Confidence = documentData.AgencyNU?.Confidence ?? 0,
                    AgencyName_Content = documentData.AgencyName?.Content,
                    AgencyName_Confidence = documentData.AgencyName?.Confidence ?? 0,
                    BM1_Address_Content = documentData.BM1_Address?.Content,
                    BM1_Address_Confidence = documentData.BM1_Address?.Confidence ?? 0,
                    BM1_Name_Content = documentData.BM1_Name?.Content,
                    BM1_Name_Confidence = documentData.BM1_Name?.Confidence ?? 0,
                    BM2_Address_Content = documentData.BM2_Address?.Content,
                    BM2_Address_Confidence = documentData.BM2_Address?.Confidence ?? 0,
                    BM2_Name_Content = documentData.BM2_Name?.Content,
                    BM2_Name_Confidence = documentData.BM2_Name?.Confidence ?? 0,
                    BM3_Address_Content = documentData.BM3_Address?.Content,
                    BM3_Address_Confidence = documentData.BM3_Address?.Confidence ?? 0,
                    BM3_Name_Content = documentData.BM3_Name?.Content,
                    BM3_Name_Confidence = documentData.BM3_Name?.Confidence ?? 0,
                    BM4_Address_Content = documentData.BM4_Address?.Content,
                    BM4_Address_Confidence = documentData.BM4_Address?.Confidence ?? 0,
                    BM4_Name_Content = documentData.BM4_Name?.Content,
                    BM4_Name_Confidence = documentData.BM4_Name?.Confidence ?? 0,
                    BM5_Address_Content = documentData.BM5_Address?.Content,
                    BM5_Address_Confidence = documentData.BM5_Address?.Confidence ?? 0,
                    BM5_Name_Content = documentData.BM5_Name?.Content,
                    BM5_Name_Confidence = documentData.BM5_Name?.Confidence ?? 0,
                    ClerkAddress_Content = documentData.ClerkAddress?.Content,
                    ClerkAddress_Confidence = documentData.ClerkAddress?.Confidence ?? 0,
                    ClerkName_Content = documentData.ClerkName?.Content,
                    ClerkName_Confidence = documentData.ClerkName?.Confidence ?? 0,
                    ClerkTitle_Content = documentData.ClerkTitle?.Content,
                    ClerkTitle_Confidence = documentData.ClerkTitle?.Confidence ?? 0,
                    Initial_Content = documentData.Initial?.Content == ":selected:",
                    Initial_Confidence = documentData.Initial?.Confidence ?? 0,
                    PresidentAddress_Content = documentData.PresidentAddress?.Content,
                    PresidentAddress_Confidence = documentData.PresidentAddress?.Confidence ?? 0,
                    PresidentName_Content = documentData.PresidentName?.Content,
                    PresidentName_Confidence = documentData.PresidentName?.Confidence ?? 0,
                    PresidentTitle_Content = documentData.PresidentTitle?.Content,
                    PresidentTitle_Confidence = documentData.PresidentTitle?.Confidence ?? 0,
                    SignedDate_Content = DateTime.TryParse(documentData.SignedDate?.Content, out DateTime signedDate) ? DateTime.SpecifyKind(signedDate, DateTimeKind.Utc) : (DateTime?)null,
                    SignedDate_Confidence = documentData.SignedDate?.Confidence ?? 0,
                    SignedName_Content = documentData.SignedName?.Content,
                    SignedName_Confidence = documentData.SignedName?.Confidence ?? 0,
                    Updated_Content = documentData.Updated?.Content == ":selected:",
                    Updated_Confidence = documentData.Updated?.Confidence ?? 0
                };

                _context.DocumentData.Add(documentEntity);
                await _context.SaveChangesAsync();

                var documentStatus = new DocumentStatus {
                    DocumentId = documentEntity.Id, // Assuming DocumentData has an Id property
                    Created = DateTime.Now,
                    Status = "review", // Initial status is "review"
                    Type = "SF-405"
                };

                _context.DocumentStatus.Add(documentStatus);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("analyzeDocument")]
        public async Task<IActionResult> AnalyzeDocument([FromForm] IFormFile file) {
            try {
                Console.WriteLine("Here");
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                Console.WriteLine("Begin analyze document");
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

                string endpoint = config.GetValue<string>("ApiEndpoint");
                string apiKey = config.GetValue<string>("ApiKey");
                string modelId = config.GetValue<string>("ApiModel");

                var credential = new AzureKeyCredential(apiKey);
                DocumentIntelligenceClient client = new DocumentIntelligenceClient(new Uri(endpoint), credential);

                //string encodedPath = Uri.EscapeUriString(new Uri(documentUri).AbsolutePath);
                //string fixedDocumentUri = documentUri.Replace(new Uri(documentUri).AbsolutePath, encodedPath);

                //Uri fileUri = new Uri(fixedDocumentUri);


                //Console.WriteLine("Here1: " + documentUri);
                //Console.WriteLine("Here2: " + fileUri.ToString());

                //var content = new AnalyzeDocumentContent() {
                //    UrlSource = fileUri,
                //};

                var content = new AnalyzeDocumentContent() {
                    Base64Source = new BinaryData(Convert.FromBase64String(Convert.ToBase64String(memoryStream.ToArray())))
                };

                //AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, fileUri);
                //AnalyzeResult result = operation.Value;

                Operation<AnalyzeResult> operation = await client.AnalyzeDocumentAsync(
                    WaitUntil.Completed,
                    modelId,
                    content
                );

                AnalyzeResult result = operation.Value;
                //Console.WriteLine(result.ToString());
                //return Content("","application/json");

                var jsonResponse = new {
                    ModelId = result.ModelId,
                    Documents = result.Documents.Select(document => new {
                        //DocumentType = document.DocumentType,
                        Fields = document.Fields.ToDictionary(
                            field => field.Key,
                            field => new {
                                Content = field.Value.Content,
                                Confidence = field.Value.Confidence
                            }
                        )
                    })
                };

                // Serialize to JSON
                string jsonString = JsonSerializer.Serialize(jsonResponse, new JsonSerializerOptions {
                    WriteIndented = true // Optional: Format the JSON with indentation for readability
                });

                // DEBUG
                Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

                foreach (AnalyzedDocument document in result.Documents) {
                    //Console.WriteLine($"Document of type: {document.DocumentType}");

                    foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields) {
                        string fieldName = fieldKvp.Key;
                        DocumentField field = fieldKvp.Value;

                        Console.WriteLine($"Field '{fieldName}': ");

                        Console.WriteLine($"  Content: '{field.Content}'");
                        Console.WriteLine($"  Confidence: '{field.Confidence}'");
                    }
                }
                // DEBUG
                // Console.WriteLine("jsonString", jsonString);
                return Content(jsonString, "application/json"); // Return JSON response
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
