﻿using AdopPix.Models;
using AdopPix.Procedure.IProcedure;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AdopPix.Procedure
{
    public class PostProcedure : IPostProcedure
    {
        // Dependencies
        private readonly IConfiguration configuration;
        private string connectionString;

        // constructor
        public PostProcedure(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = $"Server={this.configuration["AWSMySQL_Server"]};Database={this.configuration["AWSMySQL_Database"]};user={this.configuration["AWSMySQL_Username"]};password={this.configuration["AWSMySQL_Password"]};";
        }

        // generate Post ID
        private string GeneratePostId()
        {
            string[] dateTime = DateTime.Now.ToString().Split(' ');
            string[] ddmmyyyy = dateTime[0].Split('/');
            string[] hhmmss = dateTime[1].Split(':');
            return $"post-{string.Join("", ddmmyyyy)}{string.Join("", hhmmss)}";
        }

        public async Task<string> CreateAsync(Post entity)
        {
            string id = GeneratePostId();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = id;
                    command.Parameters.Add("@Title", MySqlDbType.VarChar).Value = entity.Title;
                    command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = entity.Description;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = entity.UserId;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = entity.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
            return id;
        }
        public async Task CreateImageAsync(PostImage entity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_UploadImage";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = entity.PostId;
                    command.Parameters.Add("@ImageId", MySqlDbType.VarChar).Value = entity.ImageId;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = entity.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<Post>> FindAllAsync()
        {
            List<Post> posts = new List<Post>();
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_Index_PostDetail";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        post = new Post
                        {
                            PostId = reader["PostId"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            // ตัวแปร เวลา จะต้อง Convert เป็น datetime ก่อนแล้วเอามาแปลงเป็น string
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        posts.Add(post);
                        post = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return posts;
        }

        public async Task<List<Post>> FindAllAsyncNew()
        {
            List<Post> posts = new List<Post>();
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_FindByNew";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        post = new Post
                        {
                            PostId = reader["PostId"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            // ตัวแปร เวลา จะต้อง Convert เป็น datetime ก่อนแล้วเอามาแปลงเป็น string
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        posts.Add(post);
                        post = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return posts;
        }

        public async Task<List<Post>> FindAllAsyncOld()
        {
            List<Post> posts = new List<Post>();
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_FindByOld";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        post = new Post
                        {
                            PostId = reader["PostId"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            // ตัวแปร เวลา จะต้อง Convert เป็น datetime ก่อนแล้วเอามาแปลงเป็น string
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        posts.Add(post);
                        post = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return posts;
        }

        public async Task<List<Post>> FindAllAsyncLike()
        {
            List<Post> posts = new List<Post>();
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_FindByLike";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        post = new Post
                        {
                            PostId = reader["PostId"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            // ตัวแปร เวลา จะต้อง Convert เป็น datetime ก่อนแล้วเอามาแปลงเป็น string
                            Created = Convert.ToDateTime(reader["Created"])
                        };
                        posts.Add(post);
                        post = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return posts;
        }

        public async Task<PostImage> FindImageByPostIdAsync(string postId)
        {
            // List<Post> images = new List<Post>();
            PostImage image = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_Index_Image";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        image = new PostImage
                        {
                            ImageId = reader["ImageId"].ToString(),
                            PostId = reader["PostId"].ToString(),

                        };
                        // images.Add(image);
                        // image = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return image;
        }

        public async Task<Post> FindByPostId(string postId)
        {
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_FindById";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        post = new Post
                        {
                            UserId = reader["UserId"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            PostId = reader["PostId"].ToString(),
                            Created = Convert.ToDateTime(reader["Created"]),
                        };
                    }
                    await connection.CloseAsync();
                }
            }
            return post;
        }

        public async Task DeletePostAsync(Post post)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_DeletePost";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = post.PostId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task DeleteImageAsync(PostImage postId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_DeleteImage";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postId.PostId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task UpdatePostAsync(Post entity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_UpdatePost";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = entity.PostId;
                    command.Parameters.Add("@Title", MySqlDbType.VarChar).Value = entity.Title;
                    command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = entity.Description;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task UpdateImageAsync(PostImage imageId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_UpdateImage";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = imageId.PostId;
                    command.Parameters.Add("@ImageId", MySqlDbType.VarChar).Value = imageId.ImageId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<PostImage>> GetAllImageAsync()
        {
            List<PostImage> posts = new List<PostImage>();
            PostImage post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_GetAllImage";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        post = new PostImage
                        {
                            ImageId = reader["ImageId"].ToString(),
                            PostId = reader["PostId"].ToString(),

                        };
                        posts.Add(post);
                        post = null;
                    }
                    await connection.CloseAsync();
                }
            }
            return posts;
        }

        public async Task LikeAsync(PostLike postlike)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_Like";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postlike.PostId;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = postlike.UserId;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = postlike.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task UnLikeAsync(PostLike postLike)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_UnLike";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postLike.PostId;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = postLike.UserId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<PostLike> CheckLikeStatusById(string postId,string userId)
        {
            PostLike postLike = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_ReadStatusLike";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postId;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = userId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        postLike = new PostLike
                        {
                            UserId = reader["UserId"].ToString(),
                            PostId = reader["PostId"].ToString(),

                        };
                    }
                    await connection.CloseAsync();
                }
            }
            return postLike;
        }

        public async Task<int> ShowLikeById(string postId)
        {
            int count = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_ShowLike";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postId;

                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["countLike"]);
                    }
                    await connection.CloseAsync();
                }
            }
            return count;
        }

        /*public async Task CreateCommentAsync(PostComment postComment)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_CreateComment";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CommentId", MySqlDbType.Int32).Value = postComment.CommentId;
                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postComment.PostId;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = postComment.UserId;
                    command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = postComment.Description;
                    command.Parameters.Add("@Created", MySqlDbType.DateTime).Value = postComment.Created;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task DeleteCommentAsync(PostComment postComment)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Post_DeleteComment";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CommentId", MySqlDbType.VarChar).Value = postComment.CommentId;
                    command.Parameters.Add("@PostId", MySqlDbType.VarChar).Value = postComment.PostId;

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        } */

    }
}
