using System;

namespace StoreBL
{
    public class BL : IBL
    {
        public List<StoreFront> GetAllStoreFronts { get; set; }

        private IRepo _repo;

        public BL(IRepo repo){
            _repo = repo;
        }
        public int MyProperty { get; set; }
    }
}
