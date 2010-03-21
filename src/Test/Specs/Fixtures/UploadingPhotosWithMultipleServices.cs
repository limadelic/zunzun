using fitlibrary;
using FluentSpec;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class UploadingPhotosWithMultipleServices : ConstraintFixture {
    
        public bool It_should_work_with_these_services(string ServiceName) { return Verify.That(()=> {
            
            Domain.Settings.PhotoService = ServiceName;
            
            Domain.ObjectFactory.NewPhotoWebService.Upload(Actors.Photo)
                .ShouldStartWith("http://" + ServiceName);
        });}
    }
}