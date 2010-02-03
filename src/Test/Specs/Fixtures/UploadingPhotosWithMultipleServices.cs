using fitnesse.fixtures;

namespace Zunzun.Specs.Fixtures {

    public class UploadingPhotosWithMultipleServices : TableFixture {
    
        protected override void DoStaticTable(int rows) {
            Right(1, 1);
            Right(1, 3);
        }
    }
}