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
    }
}
