using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Street = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedUserName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Email = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedEmail = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    ProviderKey = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserLogins",
                        x => new { x.LoginProvider, x.ProviderKey }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserTokens",
                        x => new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name
                        }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PreferredShippingAddressId = table.Column<int>(type: "integer", nullable: true),
                    PreferredBillingAddressId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_PreferredBillingAddressId",
                        column: x => x.PreferredBillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_PreferredShippingAddressId",
                        column: x => x.PreferredShippingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: true),
                    YearPublished = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: true),
                    PrimaryGenreId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_PrimaryGenreId",
                        column: x => x.PrimaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    OrderStatusId = table.Column<int>(type: "integer", nullable: true),
                    ShippingAddressId = table.Column<int>(type: "integer", nullable: true),
                    BillingAddressId = table.Column<int>(type: "integer", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    ShippingMethodId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_ShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethods",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_BookAuthor_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    GenreId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_BookGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    TextReview = table.Column<string>(type: "text", nullable: true),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    WishlistId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ModifiedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[]
                {
                    "Id",
                    "City",
                    "Country",
                    "CreatedAt",
                    "ModifiedAt",
                    "PostalCode",
                    "Street"
                },
                values: new object[,]
                {
                    {
                        1,
                        "New York",
                        "USA",
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(152),
                        null,
                        "12345",
                        "123 Main St"
                    },
                    {
                        2,
                        "Los Angeles",
                        "USA",
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(154),
                        null,
                        "67890",
                        "456 Elm St"
                    },
                    {
                        3,
                        "Chicago",
                        "USA",
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(156),
                        null,
                        "54321",
                        "789 Oak St"
                    },
                    {
                        4,
                        "Houston",
                        "USA",
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(157),
                        null,
                        "98765",
                        "101 Pine St"
                    },
                    {
                        5,
                        "Miami",
                        "USA",
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(157),
                        null,
                        "24680",
                        "202 Maple St"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, null, "Admin", "ADMIN" }
            );

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "AccessFailedCount",
                    "ConcurrencyStamp",
                    "CustomerId",
                    "Email",
                    "EmailConfirmed",
                    "LockoutEnabled",
                    "LockoutEnd",
                    "NormalizedEmail",
                    "NormalizedUserName",
                    "PasswordHash",
                    "PhoneNumber",
                    "PhoneNumberConfirmed",
                    "SecurityStamp",
                    "TwoFactorEnabled",
                    "UserName"
                },
                values: new object[,]
                {
                    {
                        1,
                        0,
                        "9afb4282-5f2a-47bf-a079-951fb52ea507",
                        1,
                        "test.admin@mail.com",
                        false,
                        false,
                        null,
                        "TEST.ADMIN@MAIL.COM",
                        "TEST.ADMIN",
                        "AQAAAAIAAYagAAAAEMjy2gS4mypoiogDNliVpWzAtjNPsQXEXFfZz4RtXHnK6S8ACAcaAfpj5acyuZLSpg==",
                        "5551346798",
                        false,
                        "694baa95-4931-4dde-a07f-1b32ab37cd9f",
                        false,
                        "test.admin"
                    },
                    {
                        2,
                        0,
                        "7b707214-b0ee-40a2-a20f-5f88570e5736",
                        2,
                        "test.user@mail.com",
                        false,
                        false,
                        null,
                        "TEST.USER@MAIL.COM",
                        "TEST.USER",
                        "AQAAAAIAAYagAAAAEAb+mUr+ioAgd6FqFPOQ8/WqUrjpR95ClcHFYQdtxoOKA5GdHn9AlYkxspAZpUbirw==",
                        "5553164978",
                        false,
                        "99c225bd-e3f8-4d30-b9a7-7c8c4f46ad74",
                        false,
                        "test.user"
                    },
                    {
                        3,
                        0,
                        "d365ed75-6684-4fd9-8301-46c64230f02d",
                        3,
                        "john.martin@mail.com",
                        false,
                        false,
                        null,
                        "JOHN.MARTIN@MAIL.COM",
                        "JOHN.MARTIN",
                        "AQAAAAIAAYagAAAAEKKMriFI3tzFgaGySt+Jlj1pG9lQnDH4l0N8oJroU2/B6mUdpGB4E6xonvUC7Ce7WA==",
                        "5551234567",
                        false,
                        "029b3246-ed22-4cd6-9dc5-63fd6477e446",
                        false,
                        "john.martin"
                    },
                    {
                        4,
                        0,
                        "3f822aed-ba55-4d64-9986-435c4414a437",
                        4,
                        "jane.jones@mail.com",
                        false,
                        false,
                        null,
                        "JANE.JONES@MAIL.COM",
                        "JANE.JONES",
                        "AQAAAAIAAYagAAAAEIedb16siggvgN99/piPBFbd/hVi5oI7pZc6Frb7OL9w27zadA+4LoekUwMPCLQQqQ==",
                        "5559876543",
                        false,
                        "c8c941b5-eccc-45d4-afcb-892000c8c05a",
                        false,
                        "jane.jones"
                    },
                    {
                        5,
                        0,
                        "d80beef1-ef4a-47d7-90ae-a3782999d03c",
                        5,
                        "jack.peters@mail.com",
                        false,
                        false,
                        null,
                        "JACK.PETERS@MAIL.COM",
                        "JACK.PETERS",
                        "AQAAAAIAAYagAAAAEDBoh8Gx2m3VKYVJusn2jFNKyCb/8JvxbBZcCXbrH2vqeK1jVGskqxCi7uPgmKKyuQ==",
                        "5552109876",
                        false,
                        "85413492-0a36-429b-ba33-3ffc20df8e66",
                        false,
                        "jack.peters"
                    },
                    {
                        6,
                        0,
                        "475b64ac-650c-4fd9-aced-fef7727b6ece",
                        6,
                        "jill.kowalski@mail.com",
                        false,
                        false,
                        null,
                        "JILL.KOWALSKI@MAIL.COM",
                        "JILL.KOWALSKI",
                        "AQAAAAIAAYagAAAAEFBoBcs/WWNOGNkuetCyqb0yD2HDh9yHbLkDUuEfkO/zorZqn/8eaoUqP3nrgc5rcg==",
                        "5556789054",
                        false,
                        "7c5f516d-c383-47a4-b8c6-df2ebfb0c6ca",
                        false,
                        "jill.kowalski"
                    },
                    {
                        7,
                        0,
                        "5de18394-226e-42e0-80b1-928fbf8e2b5d",
                        7,
                        "james.smith@mail.com",
                        false,
                        false,
                        null,
                        "JAMES.SMITH@MAIL.COM",
                        "JAMES.SMITH",
                        "AQAAAAIAAYagAAAAEE8jti9olIE9c+CIQb8v2VYo6CjZM0v0v9K12e10xdMOmnUFANBJFiBICj2xVP9wkQ==",
                        "5551234509",
                        false,
                        "c6e73f9b-ae13-48f3-aa9a-b58af4811df5",
                        false,
                        "james.smith"
                    },
                    {
                        8,
                        0,
                        "9d0cde9d-11b6-44f8-90e5-2da96c67088c",
                        8,
                        "jennifer.roberts@mail.com",
                        false,
                        false,
                        null,
                        "JENNIFER.ROBERTS@MAIL.COM",
                        "JENNIFER.ROBERTS",
                        "AQAAAAIAAYagAAAAEEGuWVKrhGJYnqLr8c95MUEPhylOh4wlnzYSXEG61oCimpTZw6170oj5+UWkWrLBTQ==",
                        "5550987654",
                        false,
                        "aa9e5b78-2b30-4044-aba7-bad716f4e0db",
                        false,
                        "jennifer.roberts"
                    },
                    {
                        9,
                        0,
                        "dad66fc5-7f56-4ae2-bbe5-8b9842e892db",
                        9,
                        "jeffrey.taylor@mail.com",
                        false,
                        false,
                        null,
                        "JEFFREY.TAYLOR@MAIL.COM",
                        "JEFFREY.TAYLOR",
                        "AQAAAAIAAYagAAAAECtmiSO+NKQ8tkNRtGM3zlV1keyeJhJw++OTBJD6N2edADqp23/SmDB3M+EYtKTx8Q==",
                        "5555432167",
                        false,
                        "d8631a88-6112-484b-b9bc-ed5e5213e0cd",
                        false,
                        "jeffrey.taylor"
                    },
                    {
                        10,
                        0,
                        "faef171e-7781-431a-a53c-95834692dd49",
                        10,
                        "jessica.wilson@mail.com",
                        false,
                        false,
                        null,
                        "JESSICA.WILSON@MAIL.COM",
                        "JESSICA.WILSON",
                        "AQAAAAIAAYagAAAAEI+fXOA8ZUH6bzbhGLjhu9qCYidWtqBcuhe9xSSNBH2cyTuLbpiUVDM7uoQ/tGWxlQ==",
                        "5558765432",
                        false,
                        "02e30bfb-72b4-4a91-a875-1f35143e53c9",
                        false,
                        "jessica.wilson"
                    },
                    {
                        11,
                        0,
                        "b2789352-e140-49d5-8373-d8d2a6a4f773",
                        11,
                        "jeremy.anderson@mail.com",
                        false,
                        false,
                        null,
                        "JEREMY.ANDERSON@MAIL.COM",
                        "JEREMY.ANDERSON",
                        "AQAAAAIAAYagAAAAEHQTtPN06oKZXNQ+KpdhiM02zrCtfN0wriTWAxW97ehKke1v8y3RwEAAwir7XAh8+g==",
                        "5554321098",
                        false,
                        "61e889c5-6b6b-4dc4-8af5-ad7035fd12d0",
                        false,
                        "jeremy.anderson"
                    },
                    {
                        12,
                        0,
                        "8c0e6d6c-614a-48de-8dd3-bd815aff64a8",
                        12,
                        "julia.brown@mail.com",
                        false,
                        false,
                        null,
                        "JULIA.BROWN@MAIL.COM",
                        "JULIA.BROWN",
                        "AQAAAAIAAYagAAAAEGy4qlM2GrcSNXnSitrt+ljs/3yL2vtt9mp9F/MdFZaTso9pYyqhGCg5omz1ro2kAQ==",
                        "5553210987",
                        false,
                        "d3e99141-8e63-494b-a949-03119a4c56a6",
                        false,
                        "julia.brown"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9495),
                        null,
                        "Stephen King"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9498),
                        null,
                        "J.R.R. Tolkien"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9499),
                        null,
                        "George R.R. Martin"
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9500),
                        null,
                        "Jo Nesbo"
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9500),
                        null,
                        "Agatha Christie"
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9506),
                        null,
                        "J.K. Rowling"
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9507),
                        null,
                        "Harper Lee"
                    },
                    {
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9508),
                        null,
                        "Dan Brown"
                    },
                    {
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9509),
                        null,
                        "Jane Austen"
                    },
                    {
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9510),
                        null,
                        "William Shakespeare"
                    },
                    {
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9511),
                        null,
                        "Markus Zusak"
                    },
                    {
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9511),
                        null,
                        "F. Scott Fitzgerald"
                    },
                    {
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9512),
                        null,
                        "John Green"
                    },
                    {
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9513),
                        null,
                        "Khaled Hosseini"
                    },
                    {
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9514),
                        null,
                        "Veronica Roth"
                    },
                    {
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9515),
                        null,
                        "Paulo Coelho"
                    },
                    {
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9515),
                        null,
                        "Paula Hawkins"
                    },
                    {
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9517),
                        null,
                        "Gillian Flynn"
                    },
                    {
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9518),
                        null,
                        "E.L. James"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9783),
                        null,
                        "Fantasy"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9784),
                        null,
                        "Horror"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9785),
                        null,
                        "Crime"
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9786),
                        null,
                        "Science Fiction"
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9787),
                        null,
                        "Romance"
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9788),
                        null,
                        "Mystery"
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9789),
                        null,
                        "Thriller"
                    },
                    {
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9789),
                        null,
                        "Comedy"
                    },
                    {
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9790),
                        null,
                        "Drama"
                    },
                    {
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9791),
                        null,
                        "Non-Fiction"
                    },
                    {
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9792),
                        null,
                        "Biography"
                    },
                    {
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9793),
                        null,
                        "History"
                    },
                    {
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9794),
                        null,
                        "Adventure"
                    },
                    {
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9795),
                        null,
                        "Other"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(128),
                        null,
                        "New"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(130),
                        null,
                        "Closed"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(131),
                        null,
                        "Cancelled"
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(132),
                        null,
                        "Payment Received"
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(133),
                        null,
                        "Payment Failed"
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(133),
                        null,
                        "In Progress"
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(134),
                        null,
                        "Completed"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "CreatedAt", "Description", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(137),
                        "Securely pay with your credit card.",
                        null,
                        "Credit Card"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(139),
                        "Fast and reliable payments through PayPal.",
                        null,
                        "PayPal"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(140),
                        "Seamless payments with Google Pay's convenience.",
                        null,
                        "Google Pay"
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(141),
                        "Flexible payment options with Stripe.",
                        null,
                        "Stripe"
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(141),
                        "Make easy and secure payments using Apple Pay.",
                        null,
                        "Apple Pay"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9526),
                        null,
                        "HarperCollins"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9528),
                        null,
                        "Penguin Random House"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9529),
                        null,
                        "Simon & Schuster"
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9529),
                        null,
                        "Hachette Livre"
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9530),
                        null,
                        "Macmillan Publishers"
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9531),
                        null,
                        "Scholastic Corporation"
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9532),
                        null,
                        "Pearson PLC"
                    },
                    {
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9533),
                        null,
                        "Oxford University Press"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "ShippingMethods",
                columns: new[] { "Id", "CreatedAt", "Description", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(144),
                        "DHL Express - Fast and reliable international shipping",
                        null,
                        "DHL"
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(146),
                        "UPS Ground - Economical domestic shipping",
                        null,
                        "UPS Ground"
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(147),
                        "FedEx Standard - Standard shipping service",
                        null,
                        "FedEx Standard"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 }
            );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[]
                {
                    "Id",
                    "CreatedAt",
                    "Description",
                    "ISBN",
                    "ImageUrl",
                    "ModifiedAt",
                    "Price",
                    "PrimaryGenreId",
                    "PublisherId",
                    "Title",
                    "YearPublished"
                },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9539),
                        "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction. The book remains popular and is recognized as a classic in children's literature.",
                        "9780006754024",
                        "https://m.media-amazon.com/images/I/710+HcoP38L._SY466_.jpg",
                        null,
                        24.99m,
                        1,
                        1,
                        "The Hobbit",
                        1937
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9550),
                        "A Game of Thrones is the first novel in A Song of Ice and Fire, a series of fantasy novels by the American author George R. R. Martin. It was first published on August 1, 1996. The novel won the 1997 Locus Award and was nominated for both the 1997 Nebula Award and the 1997 World Fantasy Award.",
                        "9780553103540",
                        "https://m.media-amazon.com/images/I/81GdMqla0cL._SY466_.jpg",
                        null,
                        9.99m,
                        2,
                        2,
                        "A Game of Thrones",
                        1996
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9552),
                        "The Shining is a horror novel by American author Stephen King. Published in 1977, it is King's third published novel and first hardback bestseller: the success of the book firmly established King as a preeminent author in the horror genre.",
                        "9780385121675",
                        "https://m.media-amazon.com/images/I/81QckmGleYL._SY466_.jpg",
                        null,
                        10.99m,
                        3,
                        3,
                        "The Shining",
                        1977
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9554),
                        "The Snowman is a novel by Norwegian crime-writer Jo Nesbø. It is the seventh entry in his Harry Hole series. In Australia, the title was changed to Harry Hole: The Snowman, because of another 1991 novel of the same name by J.R. Rain.",
                        "9780099520276",
                        "https://m.media-amazon.com/images/I/61uvYOfKHzL._SY466_.jpg",
                        null,
                        9.99m,
                        4,
                        4,
                        "The Snowman",
                        2007
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9556),
                        "The Return of the King is the third and final volume of J. R. R. Tolkien's The Lord of the Rings, following The Fellowship of the Ring and The Two Towers. The story begins in the kingdom of Gondor, which is soon to be attacked by the Dark Lord Sauron.",
                        "9780618260553",
                        "https://m.media-amazon.com/images/I/91tZn9CjAwL._SY466_.jpg",
                        null,
                        11.99m,
                        5,
                        1,
                        "The Return of the King",
                        1955
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9558),
                        "It is a horror novel by American author Stephen King, published in 1986. It deals with themes that eventually became King staples: the power of memory, childhood trauma, and the ugliness lurking behind a façade of traditional small-town values.",
                        "9780451169518",
                        "https://m.media-amazon.com/images/I/91jgm-KF0ZL._SL1500_.jpg",
                        null,
                        14.99m,
                        6,
                        3,
                        "It",
                        1986
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9561),
                        "The Catcher in the Rye is a novel by J.D. Salinger, published in 1951. It is a story of a young man named Holden Caulfield who embarks on a journey of self-discovery in New York City.",
                        "9780316769488",
                        "https://m.media-amazon.com/images/I/71nXPGovoTL._SL1500_.jpg",
                        null,
                        9.99m,
                        7,
                        5,
                        "The Catcher in the Rye",
                        1951
                    },
                    {
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9563),
                        "The Da Vinci Code is a mystery thriller novel by Dan Brown, published in 2003. It follows the adventures of Robert Langdon, a Harvard professor of symbology, as he tries to solve a murder that leads to a series of secrets kept by a secret society.",
                        "9780307474278",
                        "https://m.media-amazon.com/images/I/71QG6t0OOrL._SL1200_.jpg",
                        null,
                        10.99m,
                        8,
                        6,
                        "The Da Vinci Code",
                        2003
                    },
                    {
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9565),
                        "Pride and Prejudice is a novel by Jane Austen, published in 1813. It is a classic of English literature and tells the story of Elizabeth Bennet and her romantic relationship with Mr. Darcy.",
                        "9780486280575",
                        "https://m.media-amazon.com/images/I/81NLDvyAHrL._SL1500_.jpg",
                        null,
                        11.99m,
                        9,
                        8,
                        "Pride and Prejudice",
                        1813
                    },
                    {
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9662),
                        "The Great Gatsby is a novel by F. Scott Fitzgerald, published in 1925. It is a story of the American Dream, wealth, and love, set against the backdrop of the Roaring Twenties.",
                        "9780743273565",
                        "https://m.media-amazon.com/images/I/61z0MrB6qOS._SL1500_.jpg",
                        null,
                        12.99m,
                        10,
                        7,
                        "The Great Gatsby",
                        1925
                    },
                    {
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9665),
                        "The Girl with the Dragon Tattoo is a crime thriller novel by Swedish author and journalist Stieg Larsson, published in 2005. It follows the investigation into the disappearance of a wealthy industrialist's niece and delves into family secrets.",
                        "9780307269751",
                        "https://m.media-amazon.com/images/I/81UOA8fDGaL._SL1500_.jpg",
                        null,
                        16.99m,
                        11,
                        4,
                        "The Girl with the Dragon Tattoo",
                        2005
                    },
                    {
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9667),
                        "1984 is a dystopian novel by George Orwell, published in 1949. It portrays a totalitarian society where the government exercises control over every aspect of people's lives, including their thoughts and language.",
                        "9780451524935",
                        "https://m.media-amazon.com/images/I/61u1QqUfL-L._SL1500_.jpg",
                        null,
                        11.99m,
                        12,
                        5,
                        "1984",
                        1949
                    },
                    {
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9669),
                        "To Kill a Mockingbird is a novel by Harper Lee, published in 1960. It addresses issues of racism, social injustice, and moral growth as seen through the eyes of a young girl in the American South.",
                        "9780061120084",
                        "https://m.media-amazon.com/images/I/51tDHl8Z7cL.jpg",
                        null,
                        8.99m,
                        13,
                        1,
                        "To Kill a Mockingbird",
                        1960
                    },
                    {
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9671),
                        "The Alchemist is a novel by Brazilian author Paulo Coelho, originally published in Portuguese in 1988. It is a philosophical and inspirational book that tells the story of Santiago, a shepherd boy who embarks on a journey to fulfill his dreams.",
                        "9780061122415",
                        "https://m.media-amazon.com/images/I/71zHDXu1TaL._SL1500_.jpg",
                        null,
                        5.99m,
                        1,
                        2,
                        "The Alchemist",
                        1988
                    },
                    {
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9673),
                        "The Catcher in the Rye is a novel by J.D. Salinger, published in 1951. It is a story of a young man named Holden Caulfield who embarks on a journey of self-discovery in New York City.",
                        "9780316769488",
                        "https://m.media-amazon.com/images/I/91iDBLW-vHL._SL1500_.jpg",
                        null,
                        7.99m,
                        2,
                        5,
                        "The Catcher in the Rye",
                        1951
                    },
                    {
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9674),
                        "Moby-Dick; or, The Whale is an epic novel by Herman Melville, published in 1851. It tells the story of Captain Ahab's obsessive quest to kill the giant white whale, Moby-Dick.",
                        "9780143107319",
                        "https://m.media-amazon.com/images/I/71d5wo+-MuL._SL1200_.jpg",
                        null,
                        6.99m,
                        3,
                        1,
                        "Moby-Dick",
                        1851
                    },
                    {
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9676),
                        "The Road is a post-apocalyptic novel by Cormac McCarthy, published in 2006. It follows a father and son's journey through a desolate, dangerous world in search of safety.",
                        "9780307265432",
                        "https://m.media-amazon.com/images/I/51M7XGLQTBL._SL1200_.jpg",
                        null,
                        4.99m,
                        4,
                        1,
                        "The Road",
                        2006
                    },
                    {
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9680),
                        "The Hitchhiker's Guide to the Galaxy is a comedic science fiction series by Douglas Adams, first published in 1979. It follows the misadventures of Arthur Dent, an unwitting Earthling who travels the universe with an alien friend.",
                        "9780345391803",
                        "https://m.media-amazon.com/images/I/91pUhA4qZnL._SL1500_.jpg",
                        null,
                        15.99m,
                        5,
                        2,
                        "The Hitchhiker's Guide to the Galaxy",
                        1979
                    },
                    {
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9682),
                        "War and Peace is an epic historical novel by Leo Tolstoy, published in 1869. It chronicles the events of Russian society during the Napoleonic era and features a large cast of characters.",
                        "9780192833983",
                        "https://m.media-amazon.com/images/I/81bLfmgMcwL._SL1500_.jpg",
                        null,
                        10.99m,
                        6,
                        3,
                        "War and Peace",
                        1869
                    },
                    {
                        20,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9684),
                        "Brave New World is a dystopian novel by Aldous Huxley, published in 1932. It explores a future society where advanced technology has eliminated suffering, but at the cost of personal freedom and individuality.",
                        "9780060850524",
                        "https://m.media-amazon.com/images/I/71aDrgLp9CL._SL1360_.jpg",
                        null,
                        7.99m,
                        7,
                        6,
                        "Brave New World",
                        1932
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[]
                {
                    "Id",
                    "CreatedAt",
                    "Email",
                    "FirstName",
                    "LastName",
                    "ModifiedAt",
                    "PhoneNumber",
                    "PreferredBillingAddressId",
                    "PreferredShippingAddressId",
                    "UserId"
                },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9723),
                        "test.admin@mail.com",
                        "Test",
                        "Admin",
                        null,
                        "5551346798",
                        null,
                        null,
                        1
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9728),
                        "test.user@mail.com",
                        "Test",
                        "User",
                        null,
                        "5553164978",
                        null,
                        null,
                        2
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9729),
                        "john.martin@mail.com",
                        "John",
                        "Martin",
                        null,
                        "5551234567",
                        null,
                        null,
                        3
                    },
                    {
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9730),
                        "jane.jones@mail.com",
                        "Jane",
                        "Jones",
                        null,
                        "5559876543",
                        null,
                        null,
                        4
                    },
                    {
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9732),
                        "jack.peters@mail.com",
                        "Jack",
                        "Peters",
                        null,
                        "5552109876",
                        null,
                        null,
                        5
                    },
                    {
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9733),
                        "jill.kowalski@mail.com",
                        "Jill",
                        "Kowalski",
                        null,
                        "5556789054",
                        null,
                        null,
                        6
                    },
                    {
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9734),
                        "james.smith@mail.com",
                        "James",
                        "Smith",
                        null,
                        "5551234509",
                        null,
                        null,
                        7
                    },
                    {
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9735),
                        "jennifer.roberts@mail.com",
                        "Jennifer",
                        "Roberts",
                        null,
                        "5550987654",
                        null,
                        null,
                        8
                    },
                    {
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9736),
                        "jeffrey.taylor@mail.com",
                        "Jeffrey",
                        "Taylor",
                        null,
                        "5555432167",
                        null,
                        null,
                        9
                    },
                    {
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9738),
                        "jessica.wilson@mail.com",
                        "Jessica",
                        "Wilson",
                        null,
                        "5558765432",
                        null,
                        null,
                        10
                    },
                    {
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9739),
                        "jeremy.anderson@mail.com",
                        "Jeremy",
                        "Anderson",
                        null,
                        "5554321098",
                        null,
                        null,
                        11
                    },
                    {
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9779),
                        "julia.brown@mail.com",
                        "Julia",
                        "Brown",
                        null,
                        "5553210987",
                        null,
                        null,
                        12
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "Id", "AuthorId", "BookId", "CreatedAt", "ModifiedAt" },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9827),
                        null
                    },
                    {
                        2,
                        3,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9829),
                        null
                    },
                    {
                        3,
                        5,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9830),
                        null
                    },
                    {
                        4,
                        2,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9831),
                        null
                    },
                    {
                        5,
                        3,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9832),
                        null
                    },
                    {
                        6,
                        6,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9833),
                        null
                    },
                    {
                        7,
                        1,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9834),
                        null
                    },
                    {
                        8,
                        2,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9835),
                        null
                    },
                    {
                        9,
                        2,
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9835),
                        null
                    },
                    {
                        10,
                        4,
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9837),
                        null
                    },
                    {
                        11,
                        5,
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9838),
                        null
                    },
                    {
                        12,
                        6,
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9839),
                        null
                    },
                    {
                        13,
                        7,
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9839),
                        null
                    },
                    {
                        14,
                        1,
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9840),
                        null
                    },
                    {
                        15,
                        5,
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9841),
                        null
                    },
                    {
                        16,
                        5,
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9842),
                        null
                    },
                    {
                        17,
                        3,
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9843),
                        null
                    },
                    {
                        18,
                        7,
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9844),
                        null
                    },
                    {
                        19,
                        4,
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9845),
                        null
                    },
                    {
                        20,
                        1,
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9846),
                        null
                    },
                    {
                        21,
                        2,
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9847),
                        null
                    },
                    {
                        22,
                        6,
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9848),
                        null
                    },
                    {
                        23,
                        7,
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9849),
                        null
                    },
                    {
                        24,
                        5,
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9849),
                        null
                    },
                    {
                        25,
                        3,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9850),
                        null
                    },
                    {
                        26,
                        7,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9851),
                        null
                    },
                    {
                        27,
                        2,
                        20,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9852),
                        null
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "Id", "BookId", "CreatedAt", "GenreId", "ModifiedAt" },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9797),
                        1,
                        null
                    },
                    {
                        2,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9800),
                        3,
                        null
                    },
                    {
                        3,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9802),
                        5,
                        null
                    },
                    {
                        4,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9803),
                        2,
                        null
                    },
                    {
                        5,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9804),
                        3,
                        null
                    },
                    {
                        6,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9805),
                        6,
                        null
                    },
                    {
                        7,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9806),
                        4,
                        null
                    },
                    {
                        8,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9807),
                        2,
                        null
                    },
                    {
                        9,
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9808),
                        2,
                        null
                    },
                    {
                        10,
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9809),
                        10,
                        null
                    },
                    {
                        11,
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9810),
                        5,
                        null
                    },
                    {
                        12,
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9811),
                        6,
                        null
                    },
                    {
                        13,
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9812),
                        11,
                        null
                    },
                    {
                        14,
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9813),
                        1,
                        null
                    },
                    {
                        15,
                        9,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9813),
                        5,
                        null
                    },
                    {
                        16,
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9814),
                        5,
                        null
                    },
                    {
                        17,
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9815),
                        8,
                        null
                    },
                    {
                        18,
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9816),
                        5,
                        null
                    },
                    {
                        19,
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9817),
                        11,
                        null
                    },
                    {
                        20,
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9818),
                        1,
                        null
                    },
                    {
                        21,
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9819),
                        8,
                        null
                    },
                    {
                        22,
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9820),
                        6,
                        null
                    },
                    {
                        23,
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9821),
                        7,
                        null
                    },
                    {
                        24,
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9822),
                        9,
                        null
                    },
                    {
                        25,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9822),
                        3,
                        null
                    },
                    {
                        26,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9823),
                        12,
                        null
                    },
                    {
                        27,
                        20,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9824),
                        10,
                        null
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[]
                {
                    "Id",
                    "BillingAddressId",
                    "CreatedAt",
                    "CustomerId",
                    "IsPaid",
                    "ModifiedAt",
                    "Note",
                    "OrderDate",
                    "OrderStatusId",
                    "PaymentMethodId",
                    "ShippingAddressId",
                    "ShippingMethodId",
                    "TotalPrice"
                },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9857),
                        1,
                        true,
                        null,
                        null,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9856),
                        1,
                        null,
                        1,
                        null,
                        24.99m
                    },
                    {
                        2,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9863),
                        2,
                        false,
                        null,
                        null,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9863),
                        2,
                        null,
                        2,
                        null,
                        31.97m
                    },
                    {
                        3,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9865),
                        3,
                        true,
                        null,
                        null,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9864),
                        3,
                        null,
                        3,
                        null,
                        11.99m
                    },
                    {
                        4,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9901),
                        4,
                        true,
                        null,
                        null,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9900),
                        1,
                        null,
                        4,
                        null,
                        29.98m
                    },
                    {
                        5,
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9903),
                        5,
                        false,
                        null,
                        null,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9903),
                        2,
                        null,
                        5,
                        null,
                        61.94m
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[]
                {
                    "Id",
                    "BookId",
                    "CreatedAt",
                    "CustomerId",
                    "ModifiedAt",
                    "Rating",
                    "TextReview"
                },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9689),
                        3,
                        null,
                        5,
                        "This book is amazing!"
                    },
                    {
                        2,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9693),
                        4,
                        null,
                        4,
                        "This book is pretty good!"
                    },
                    {
                        3,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9694),
                        3,
                        null,
                        3,
                        "This book is ok!"
                    },
                    {
                        4,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9696),
                        3,
                        null,
                        2,
                        "This book is pretty bad!"
                    },
                    {
                        5,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9697),
                        7,
                        null,
                        1,
                        "This book is terrible!"
                    },
                    {
                        6,
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9698),
                        9,
                        null,
                        4,
                        "An enchanting read that captivated my imagination!"
                    },
                    {
                        7,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9699),
                        10,
                        null,
                        3,
                        "Decent book, but not my favorite."
                    },
                    {
                        8,
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9700),
                        5,
                        null,
                        5,
                        "A gripping page-turner that held my attention till the end!"
                    },
                    {
                        9,
                        20,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9701),
                        10,
                        null,
                        2,
                        "Disappointing book, didn't live up to the hype."
                    },
                    {
                        10,
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9703),
                        3,
                        null,
                        4,
                        "An enjoyable and thought-provoking literary journey."
                    },
                    {
                        11,
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9704),
                        6,
                        null,
                        5,
                        "This book is a masterpiece that will stay with me forever!"
                    },
                    {
                        12,
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9705),
                        7,
                        null,
                        4,
                        "A classic that never gets old, a must-read!"
                    },
                    {
                        13,
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9706),
                        8,
                        null,
                        3,
                        "It was an interesting story, but it didn't fully resonate with me."
                    },
                    {
                        14,
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9707),
                        1,
                        null,
                        5,
                        "A compelling narrative with well-developed characters!"
                    },
                    {
                        15,
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9708),
                        4,
                        null,
                        4,
                        "This book made me ponder the meaning of life."
                    },
                    {
                        16,
                        19,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9709),
                        9,
                        null,
                        4,
                        "A thought-provoking and engaging story that kept me hooked!"
                    },
                    {
                        17,
                        18,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9710),
                        6,
                        null,
                        3,
                        "Decent read, but not a personal favorite."
                    },
                    {
                        18,
                        17,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9711),
                        3,
                        null,
                        5,
                        "This book is a literary gem, a must-read!"
                    },
                    {
                        19,
                        20,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9712),
                        8,
                        null,
                        2,
                        "A disappointment, didn't live up to expectations."
                    },
                    {
                        20,
                        12,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9713),
                        1,
                        null,
                        4,
                        "An unforgettable narrative with well-developed characters."
                    },
                    {
                        21,
                        16,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9714),
                        4,
                        null,
                        5,
                        "This book is a true masterpiece of literature!"
                    },
                    {
                        22,
                        13,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9715),
                        10,
                        null,
                        4,
                        "A classic that continues to captivate readers."
                    },
                    {
                        23,
                        15,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9716),
                        5,
                        null,
                        3,
                        "An interesting story, but not my top choice."
                    },
                    {
                        24,
                        14,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9717),
                        7,
                        null,
                        5,
                        "This book was a delightful journey with rich characters."
                    },
                    {
                        25,
                        11,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9718),
                        2,
                        null,
                        4,
                        "A philosophical exploration that left me thinking."
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedAt" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(163),
                        1,
                        null
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(167),
                        2,
                        null
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedAt" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(111),
                        1,
                        null
                    },
                    {
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(114),
                        2,
                        null
                    },
                    {
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(114),
                        3,
                        null
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[]
                {
                    "Id",
                    "BookId",
                    "CreatedAt",
                    "ModifiedAt",
                    "OrderId",
                    "Quantity",
                    "TotalPrice"
                },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9908),
                        null,
                        1,
                        1,
                        24.99m
                    },
                    {
                        2,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9918),
                        null,
                        2,
                        1,
                        9.99m
                    },
                    {
                        3,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9919),
                        null,
                        2,
                        2,
                        21.99m
                    },
                    {
                        4,
                        5,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9920),
                        null,
                        3,
                        1,
                        11.99m
                    },
                    {
                        5,
                        6,
                        new DateTime(2024, 1, 17, 20, 3, 13, 45, DateTimeKind.Utc).AddTicks(9921),
                        null,
                        4,
                        2,
                        29.98m
                    },
                    {
                        6,
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(102),
                        null,
                        5,
                        4,
                        39.96m
                    },
                    {
                        7,
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(104),
                        null,
                        5,
                        2,
                        21.98m
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[]
                {
                    "Id",
                    "BookId",
                    "CreatedAt",
                    "ModifiedAt",
                    "Quantity",
                    "ShoppingCartId"
                },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(169),
                        null,
                        1,
                        1
                    },
                    {
                        2,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(173),
                        null,
                        1,
                        1
                    },
                    {
                        3,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(174),
                        null,
                        1,
                        1
                    },
                    {
                        4,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(175),
                        null,
                        2,
                        2
                    },
                    {
                        5,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(176),
                        null,
                        1,
                        2
                    },
                    {
                        6,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(178),
                        null,
                        1,
                        2
                    },
                    {
                        7,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(179),
                        null,
                        1,
                        2
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "WishlistItems",
                columns: new[] { "Id", "BookId", "CreatedAt", "ModifiedAt", "WishlistId" },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(117),
                        null,
                        1
                    },
                    {
                        2,
                        8,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(119),
                        null,
                        1
                    },
                    {
                        3,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(119),
                        null,
                        1
                    },
                    {
                        4,
                        4,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(120),
                        null,
                        2
                    },
                    {
                        5,
                        7,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(121),
                        null,
                        2
                    },
                    {
                        6,
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(123),
                        null,
                        2
                    },
                    {
                        7,
                        3,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(123),
                        null,
                        3
                    },
                    {
                        8,
                        2,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(124),
                        null,
                        3
                    },
                    {
                        9,
                        10,
                        new DateTime(2024, 1, 17, 20, 3, 13, 46, DateTimeKind.Utc).AddTicks(125),
                        null,
                        3
                    }
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookId_AuthorId",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_BookId_GenreId",
                table: "BookGenre",
                columns: new[] { "BookId", "GenreId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenreId",
                table: "BookGenre",
                column: "GenreId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PreferredBillingAddressId",
                table: "Customers",
                column: "PreferredBillingAddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PreferredShippingAddressId",
                table: "Customers",
                column: "PreferredShippingAddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId_OrderId",
                table: "OrderItems",
                columns: new[] { "BookId", "OrderId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId_CustomerId",
                table: "Reviews",
                columns: new[] { "BookId", "CustomerId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_BookId_ShoppingCartId",
                table: "ShoppingCartItems",
                columns: new[] { "BookId", "ShoppingCartId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId",
                table: "ShoppingCarts",
                column: "CustomerId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_BookId_WishlistId",
                table: "WishlistItems",
                columns: new[] { "BookId", "WishlistId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems",
                column: "WishlistId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CustomerId",
                table: "Wishlists",
                column: "CustomerId",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.DropTable(name: "AspNetUserClaims");

            migrationBuilder.DropTable(name: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetUserRoles");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropTable(name: "BookAuthor");

            migrationBuilder.DropTable(name: "BookGenre");

            migrationBuilder.DropTable(name: "OrderItems");

            migrationBuilder.DropTable(name: "Reviews");

            migrationBuilder.DropTable(name: "ShoppingCartItems");

            migrationBuilder.DropTable(name: "WishlistItems");

            migrationBuilder.DropTable(name: "AspNetRoles");

            migrationBuilder.DropTable(name: "Authors");

            migrationBuilder.DropTable(name: "Orders");

            migrationBuilder.DropTable(name: "ShoppingCarts");

            migrationBuilder.DropTable(name: "Books");

            migrationBuilder.DropTable(name: "Wishlists");

            migrationBuilder.DropTable(name: "OrderStatuses");

            migrationBuilder.DropTable(name: "PaymentMethods");

            migrationBuilder.DropTable(name: "ShippingMethods");

            migrationBuilder.DropTable(name: "Genres");

            migrationBuilder.DropTable(name: "Publishers");

            migrationBuilder.DropTable(name: "Customers");

            migrationBuilder.DropTable(name: "Addresses");

            migrationBuilder.DropTable(name: "AspNetUsers");
        }
    }
}
