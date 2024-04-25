using AutoMapper;
using BusinessLayer.DTOs.Requests.Auth;
using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.DTOs.Responses.Order;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.DTOs.Responses.ShoppingCart;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;
using BusinessLayer.DTOs.Responses.Wishlist;
using BusinessLayer.DTOs.Responses.WishlistItem;
using DataAccessLayer.Entities;
using WebMVC.Areas.Admin.Models.Account;
using WebMVC.Areas.Admin.Models.Author;
using WebMVC.Areas.Admin.Models.Book;
using WebMVC.Areas.Admin.Models.Genre;
using WebMVC.Areas.Admin.Models.Order;
using WebMVC.Areas.Admin.Models.Publisher;
using WebMVC.Models.Account;
using WebMVC.Models.Book;
using WebMVC.Models.Order;
using WebMVC.Models.ShoppingCart;
using WebMVC.Models.Wishlist;

namespace WebMVC;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Book
        CreateMap<BookResponse, BookDetailViewModel>();
        CreateMap<BookResponse, BookUpdateViewModel>();
        CreateMap<BookResponse, Book>();
        CreateMap<BookCreateViewModel, BookRequest>();
        CreateMap<BookUpdateViewModel, BookRequest>();

        //Genre
        CreateMap<GenreResponse, GenreUpdateViewModel>();
        CreateMap<GenreCreateViewModel, GenreRequest>();
        CreateMap<GenreUpdateViewModel, GenreRequest>();

        //Publisher
        CreateMap<PublisherResponse, PublisherUpdateViewModel>();
        CreateMap<PublisherCreateViewModel, PublisherRequest>();
        CreateMap<PublisherUpdateViewModel, PublisherRequest>();

        //Author
        CreateMap<AuthorResponse, AuthorUpdateViewModel>();
        CreateMap<AuthorCreateViewModel, AuthorRequest>();
        CreateMap<AuthorUpdateViewModel, AuthorRequest>();

        //Order
        CreateMap<Order, OrderDetailViewModel>();
        CreateMap<OrderResponse, OrderDetailViewModel>();
        CreateMap<OrderResponse, OrderUpdateViewModel>();
        CreateMap<OrderUpdateViewModel, OrderEditRequest>();

        //Shopping Cart
        CreateMap<ShoppingCartResponse, ShoppingCartDetailViewModel>();
        CreateMap<ShoppingCart, ShoppingCartDetailViewModel>();

        //Shopping Cart Item
        CreateMap<ShoppingCartItemResponse, ShoppingCartItem>();

        //Wishlist
        CreateMap<WishlistResponse, WishlistDetailViewModel>();
        CreateMap<Wishlist, WishlistDetailViewModel>();

        //Wishlist Item
        CreateMap<WishlistItemResponse, WishlistItem>();

        //User
        CreateMap<LocalIdentityUser, ResetPasswordModel>();

        //Account
        CreateMap<LoginViewModel, LoginRequest>();
        CreateMap<RegisterViewModel, RegisterRequest>();
        CreateMap<LocalIdentityUser, ProfileViewModel>();

        //...
        CreateMap<ShoppingCartItem, OrderItemRequest>();
        CreateMap<ShoppingCartItemResponse, OrderItemRequest>();

        CreateMap<WishlistItemResponse, ShoppingCartItemRequest>();
    }
}
