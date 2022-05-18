using AdopPix.Models;
using AdopPix.Models.ViewModels;
using AdopPix.Procedure.IProcedure;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopPix.Procedure
{
    public class AuctionProcedure : IAuctionProcedure
    {
        private readonly IConfiguration configuration;
        private string connectionString;
        public AuctionProcedure(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = $"Server={this.configuration["AWSMySQL_Server"]};Database={this.configuration["AWSMySQL_Database"]};user={this.configuration["AWSMySQL_Username"]};password={this.configuration["AWSMySQL_Password"]}";
        }
        private string GenerateAuctionId()
        {
            string[] dateTime = DateTime.Now.ToString().Split(' ');
            string[] ddmmyyyy = dateTime[0].Split('/');
            string[] hhmmss = dateTime[1].Split(':');
            return $"auction-{string.Join("", ddmmyyyy)}{string.Join("", hhmmss)}";
        }
        public async Task CreateAsync(Auction auction)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var generateAuctionId = GenerateAuctionId();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    
                    command.CommandText = "Auction_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auction.AuctionId;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = auction.UserId;
                    command.Parameters.Add("@Title", MySqlDbType.VarChar).Value = auction.Title;
                    command.Parameters.Add("@HourId", MySqlDbType.Int32).Value = auction.HourId;
                    command.Parameters.Add("@OpeningPrice", MySqlDbType.Decimal).Value = auction.OpeningPrice;
                    command.Parameters.Add("@HotClose", MySqlDbType.Decimal).Value = auction.HotClose;
                    command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = auction.Description;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = auction.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();

                    
                }

            }
        }

        public async Task DeleteAuctionAsync(Auction auction)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_Delete";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auction.AuctionId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task DeleteImageAsync(AuctionImage auctionImage)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_DeleteImage";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionImage.AuctionId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public Task FindAll(Auction auction)
        {
            throw new NotImplementedException();
        }

        public async Task<Auction> FindByIdAsync(string auctionId)
        {
            Auction auction = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionId_FindById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        auction = new Auction
                        {
                            AuctionId = reader["AuctionId"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            Title = reader["Title"].ToString(),
                            HourId = Convert.ToInt32(reader["HourId"].ToString()),
                            Created = Convert.ToDateTime(reader["Created"].ToString()),
                            OpeningPrice = Convert.ToDecimal(reader["OpeningPrice"].ToString()),
                            HotClose = Convert.ToDecimal(reader["HotClose"].ToString()),
                            Description = reader["Description"].ToString(),
                            Status = Convert.ToInt32(reader["Status"]),
                        };
                        auction.StartTime = (await reader.IsDBNullAsync(reader.GetOrdinal("StartTime"))) ? null : Convert.ToDateTime(reader["StartTime"]);
                        auction.StopTime = (await reader.IsDBNullAsync(reader.GetOrdinal("StopTime"))) ? null : Convert.ToDateTime(reader["StopTime"]);
                    }
                    await connection.CloseAsync();
                }
            }
            return auction;
        }
        public async Task<AuctionImage> FindImageByIdAsync(string auctionId)
        {
            AuctionImage auctionImage = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionId_FindImageById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        auctionImage = new AuctionImage
                        {
                            ImageId = reader["ImageId"].ToString(),
                            AuctionId = reader["AuctionId"].ToString()

                        };
                    }
                    await connection.CloseAsync();
                }
            }
            return auctionImage;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task UpdateAuctionAsync(Auction entity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_Update";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = entity.AuctionId;
                    command.Parameters.Add("@Title", MySqlDbType.VarChar).Value = entity.Title;
                    command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = entity.Description;
                    command.Parameters.Add("@StatusAuc", MySqlDbType.Int32).Value = entity.Status;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }
        //public async Task UpdateImageAsync(AuctionImage imageId)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText = "Auction_UpdateImage";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = imageId.AuctionId;
        //            command.Parameters.Add("@ImageId", MySqlDbType.VarChar).Value = imageId.ImageId;

        //            await connection.OpenAsync();
        //            await command.ExecuteNonQueryAsync();
        //            await connection.CloseAsync();
        //        }
        //    }
        //}



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task CreateImageAsync(AuctionImage auctionImage)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionImage_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ImageId", MySqlDbType.VarChar).Value = auctionImage.ImageId;
                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionImage.AuctionId;
                    command.Parameters.Add("@ImageTypeId", MySqlDbType.Int32).Value = 1;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = auctionImage.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }


        public async Task<List<Auction>> GetAllAsync()
        {
            List<Auction> auctions = new List<Auction>();
            Auction auction = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_GetAll";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        auction = new Auction
                        {
                            AuctionId = reader["AuctionId"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            Title = reader["Title"].ToString(),
                            HourId = Convert.ToInt32(reader["HourId"].ToString()),
                            Created = Convert.ToDateTime(reader["Created"]),
                            OpeningPrice = Convert.ToDecimal(reader["OpeningPrice"].ToString()),
                            HotClose = Convert.ToDecimal(reader["HotClose"].ToString()),
                            Description = reader["Description"].ToString(),
                            Status = Convert.ToInt32(reader["Status"])

                        };
                        auction.StartTime = (await reader.IsDBNullAsync(reader.GetOrdinal("StartTime"))) ? null : Convert.ToDateTime(reader["StartTime"]);
                        auction.StopTime = (await reader.IsDBNullAsync(reader.GetOrdinal("StopTime"))) ? null : Convert.ToDateTime(reader["StopTime"]);
                        auctions.Add(auction);
                        auction = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return auctions;
        }
        public async Task<List<AuctionImage>> GetAllImageAsync()
        {
            List<AuctionImage> auctionImages = new List<AuctionImage>();
            AuctionImage auctionimage = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_GetAllImage";
                    command.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        auctionimage = new AuctionImage
                        {
                            ImageId = reader["ImageId"].ToString(),
                            AuctionId = reader["AuctionId"].ToString(),
                        };
                        auctionImages.Add(auctionimage);
                        auctionimage = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return auctionImages;
        }

        public async Task<List<UserProfile>> GetAllUserImageDetailAsync()
        {
            List<UserProfile> auctionUserImages = new List<UserProfile>();
            UserProfile auctionUserimage = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_GetAllUserImageDetails";
                    command.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        auctionUserimage = new UserProfile
                        {
                            AvatarName = reader["AvatarName"].ToString(),
                            UserId = reader["UserId"].ToString(),
                        };
                        auctionUserImages.Add(auctionUserimage);
                        auctionUserimage = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return auctionUserImages;
        }
        public async Task<List<User>> GetAllUserDetailAsync()
        {
            List<User> auctionImages = new List<User>();
            User auctionimage = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_GetAllUserDetails";
                    command.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        auctionimage = new User
                        {
                            UserName = reader["UserName"].ToString(),
                            Id = reader["Id"].ToString(),
                        };
                        auctionImages.Add(auctionimage);
                        auctionimage = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return auctionImages;
        }



        public async Task<AuctionViewModel> FindByUserIdAsync(string userId)
        {
            AuctionViewModel userProfile = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UserName_FindById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = userId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userProfile = new AuctionViewModel
                        {
                            UserName = reader["UserName"].ToString(),
                        };
                    }
                    await connection.CloseAsync();
                }
            }
            return userProfile;
        }

        public async Task InitialTime(string auctionId, DateTime startTime, DateTime stopTime)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auctions_InitialTime";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;
                    command.Parameters.Add("@StartTime", MySqlDbType.DateTime).Value = startTime;
                    command.Parameters.Add("@StopTime", MySqlDbType.DateTime).Value = stopTime;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task WinningBidderCreate(WinningBidder entity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "WinningBidders_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = entity.UserId;
                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = entity.AuctionId;
                    command.Parameters.Add("@Amount", MySqlDbType.Decimal).Value = entity.amount;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = entity.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<WinningBidder> WinningBidderFindByAuctionId(string auctionId)
        {
            WinningBidder winningBidder = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "WinningBidders_FindByAuctionId";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        winningBidder = new WinningBidder()
                        {
                            UserId = reader["UserId"].ToString(),
                            AuctionId = reader["AuctionId"].ToString(),
                            amount = Convert.ToDecimal(reader["Amount"]),
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                    }
                    await connection.CloseAsync();
                }
            }

            return winningBidder;
        }

        public async Task<List<WinningBidder>> GetAllAuctionEnd()
        {
            List<WinningBidder> auctionEnds = new List<WinningBidder>();
            WinningBidder auctionEnd = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Auction_GetAllEnd";
                    command.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        auctionEnd = new WinningBidder
                        {
                            UserId = reader["UserId"].ToString(),
                            AuctionId = reader["AuctionId"].ToString(),
                            amount = Convert.ToDecimal(reader["Amount"]),
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        auctionEnds.Add(auctionEnd);
                        auctionEnd = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return auctionEnds;
        }
    }
}
