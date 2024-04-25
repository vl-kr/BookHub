using AutoMapper;
using BusinessLayer.DTOs.Requests.Address;
using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.DTOs.Requests.Customer;
using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.DTOs.Requests.LocalIdentityUser;
using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.DTOs.Requests.OrderStatus;
using BusinessLayer.DTOs.Requests.PaymentMethod;
using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.DTOs.Requests.ShippingMethod;
using BusinessLayer.DTOs.Requests.ShoppingCart;
using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.DTOs.Requests.Wishlist;
using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.DTOs.Responses.Address;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.DTOs.Responses.LocalIdentityUser;
using BusinessLayer.DTOs.Responses.Order;
using BusinessLayer.DTOs.Responses.OrderItem;
using BusinessLayer.DTOs.Responses.OrderStatus;
using BusinessLayer.DTOs.Responses.PaymentMethod;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.DTOs.Responses.Review;
using BusinessLayer.DTOs.Responses.ShippingMethod;
using BusinessLayer.DTOs.Responses.ShoppingCart;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;
using BusinessLayer.DTOs.Responses.Wishlist;
using BusinessLayer.DTOs.Responses.WishlistItem;
using DataAccessLayer.Entities;

namespace BusinessLayer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Requests
        CreateMap<BookRequest, Book>();
        CreateMap<OrderRequest, Order>();
        CreateMap<OrderEditRequest, Order>();
        CreateMap<OrderItemRequest, OrderItem>();
        CreateMap<OrderStatusRequest, OrderStatus>();
        CreateMap<AuthorRequest, Author>();
        CreateMap<CustomerRequest, Customer>();
        CreateMap<GenreRequest, Genre>();
        CreateMap<PublisherRequest, Publisher>();
        CreateMap<ReviewRequest, Review>();
        CreateMap<WishlistRequest, Wishlist>();
        CreateMap<WishlistItemRequest, WishlistItem>();
        CreateMap<PaymentMethodRequest, PaymentMethod>();
        CreateMap<ShippingMethodRequest, ShippingMethod>();
        CreateMap<AddressRequest, Address>();
        CreateMap<ShoppingCartRequest, ShoppingCart>();
        CreateMap<ShoppingCartItemRequest, ShoppingCartItem>();
        CreateMap<LocalIdentityUserRequest, LocalIdentityUser>();

        // Responses
        CreateMap<Book, BookResponse>();
        CreateMap<Book, BookBasicInfoResponse>();
        CreateMap<Order, OrderResponse>();
        CreateMap<Order, OrderBasicInfoResponse>();
        CreateMap<OrderItem, OrderItemResponse>();
        CreateMap<OrderStatus, OrderStatusResponse>();
        CreateMap<Author, AuthorResponse>();
        CreateMap<Customer, CustomerResponse>();
        CreateMap<Customer, CustomerBasicInfoResponse>();
        CreateMap<Genre, GenreResponse>();
        CreateMap<Publisher, PublisherResponse>();
        CreateMap<Review, ReviewResponse>();
        CreateMap<Review, ReviewBasicInfoResponse>();
        CreateMap<Wishlist, WishlistResponse>();
        CreateMap<WishlistItem, WishlistItemResponse>();
        CreateMap<PaymentMethod, PaymentMethodResponse>();
        CreateMap<ShippingMethod, ShippingMethodResponse>();
        CreateMap<Address, AddressResponse>();
        CreateMap<ShoppingCart, ShoppingCartResponse>();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
        CreateMap<LocalIdentityUser, LocalIdentityUserResponse>();
    }
}
