namespace CustomerLibrary.Data.EFRepositories
{
    public static class CustomerLibraryContextProvider
    {
        private static CustomerLibraryContext _context;

        public static CustomerLibraryContext Current
        {
            get => _context = _context ?? new CustomerLibraryContext();
            set => _context = value;
        }
    }
}