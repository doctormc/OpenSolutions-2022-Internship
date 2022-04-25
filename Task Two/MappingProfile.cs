namespace TaskTwo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameDB, GameView>(MemberList.Destination);

            CreateMap<GameView, GameDB>(MemberList.Destination);

            CreateMap<GameAdd, GameView>(MemberList.Destination);

            CreateMap<ReviewDB, ReviewView>();

            CreateMap<ReviewView, ReviewDB>()
                .ForMember("Text", o => o.NullSubstitute(" "));

            CreateMap<User, UserView>();

            CreateMap<User, UserView>().ReverseMap();

        }
    }
}
