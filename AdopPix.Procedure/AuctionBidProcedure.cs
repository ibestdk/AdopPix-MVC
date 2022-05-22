using AdopPix.Models;
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
    public class AuctionBidProcedure : IAuctionBidProcedure
    {
        private readonly IConfiguration configuration;
        private string connectionString;
        public AuctionBidProcedure(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = $"Server={this.configuration["AWSMySQL_Server"]};Database={this.configuration["AWSMySQL_Database"]};user={this.configuration["AWSMySQL_Username"]};password={this.configuration["AWSMySQL_Password"]}";
        }
        public async Task Create(AuctionBid entity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionBids_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = entity.UserId;
                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = entity.AuctionId;
                    command.Parameters.Add("@Amount", MySqlDbType.Decimal).Value = entity.Amount;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = entity.Created;

                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<AuctionBidFindByAuctionId>> FindByAuctionId(string auctionId)
        {
            List<AuctionBidFindByAuctionId> results = new List<AuctionBidFindByAuctionId>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionBids_FindByAuctionId";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;

                    await connection.OpenAsync();

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AuctionBidFindByAuctionId auctionBidFindByAuctionId = new AuctionBidFindByAuctionId()
                        {
                            AvatarName = reader["AvatarName"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            UserName = reader["UserName"].ToString(),
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        results.Add(auctionBidFindByAuctionId);
                    }
                    await connection.CloseAsync();
                }
            }
            if(results.Count == 0)
            {
                return null;
            }
            return results;
        }

        public async Task<AuctionBid> FindMaxAmountByAuctionId(string auctionId)
        {
            AuctionBid auctionBid = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionBids_FindMaxAmountByAuctionId";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;

                    await connection.OpenAsync();

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        auctionBid = new AuctionBid()
                        {
                            Amount = Convert.ToDecimal(reader["maxAmount"]),
                            UserId = reader["UserId"].ToString()
                        };
                    }

                    await connection.CloseAsync();
                }
            }
            return auctionBid;
        }

        public async Task<List<AuctionBid>> FindUserLoseAuction(string auctionId)
        {
            List<AuctionBid> auctionBids = new List<AuctionBid>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AuctionBid_FindUserLoseAuction";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AuctionId", MySqlDbType.VarChar).Value = auctionId;

                    await connection.OpenAsync();

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var auctionBid = new AuctionBid()
                        {
                            Amount = Convert.ToDecimal(reader["AmountSum"]),
                            UserId = reader["UserId"].ToString()
                        };
                        auctionBids.Add(auctionBid);
                    }

                    await connection.CloseAsync();
                }
            }
            return auctionBids;
        }
    }
}
