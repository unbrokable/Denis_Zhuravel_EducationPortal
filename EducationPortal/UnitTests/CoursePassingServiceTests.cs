using Application.Interfaces;
using Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Domain;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;
using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces.IServices;
using Domain.Specification;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CoursePassingServiceTests
    {
        Mock<IEntitiesRepository> repository;
        Mock<IServiceUser> serviceUser;
        Mock<IServiceCourse> serviceCourse;
        Mock<IServiceMaterial> serviceMaterial;

        Specification<T> GetTrueSpecification<T>() where T:class => new Specification<T>(i => true);

        [TestInitialize]
        public void SetupInterfaces()
        {
            repository = new Mock<IEntitiesRepository>();
            serviceCourse = new Mock<IServiceCourse>();
            serviceMaterial = new Mock<IServiceMaterial>();
            serviceUser = new Mock<IServiceUser>();
        } 

        [TestMethod]
        public void ChooseCourse_InvalidUserId_ReturnFalse()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult( new CourseDTO()));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((UserDTO)null));
            repository.Setup(i => i.AddAsync<CompositionPassedMaterial>(It.IsAny<CompositionPassedMaterial>())).Returns(Task.FromResult(true));
            ICoursePassingService service = new CoursePassingService(repository.Object,serviceMaterial.Object,serviceCourse.Object,serviceUser.Object);
            
            var expected = false;

            var result = service.ChooseCourseAsync(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ChooseCourse_InvalidCourseId_ReturnFalse()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult( (CourseDTO)null));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((new UserDTO())));
            repository.Setup(i => i.AddAsync<CompositionPassedMaterial>(It.IsAny<CompositionPassedMaterial>())).Returns(Task.FromResult(true));
            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);
           
            var expected = false;

            var result = service.ChooseCourseAsync(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ChooseCourse_RightCourseIdAndUserId_ReturnTrue()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new CourseDTO()));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new UserDTO() ));
            repository.Setup(i => i.AddAsync<CompositionPassedCourse>(It.IsAny<CompositionPassedCourse>())).Returns(Task.FromResult(true));

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var expected = true;

            var result = service.ChooseCourseAsync(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_InvalidCourseIdAndUserId_ReturnsNull()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((CourseDTO)null));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((UserDTO)null));
            repository.Setup(i => i.FindAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null)).Returns(Task.FromResult(new CompositionPassedMaterial { }));
            object expected = null;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourseAsync(-1, -1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_RightCourseIdAndUserIdButUserDontTakeCourse_ReturnsNull()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new CourseDTO()));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult( (new UserDTO())));
            repository.Setup(i => i.FindAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null))
                .Returns(Task.FromResult((CompositionPassedMaterial)null));
            object expected = null;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourseAsync(1, 1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_RightCourseIdAndUserId_ReturnsCourseProgressDTO()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult( new CourseDTO()
            {
                Id = 1,
                Materials = new List<MaterialDTO> {
                    new VideoMaterialDTO() {Id = 1},
                    new ArticleMaterialDTO(){Id = 2},
                    new ArticleMaterialDTO(){Id = 3},
                    new BookMaterialDTO(){Id = 5},
                }
            }));
            serviceUser.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((new UserDTO())));
            repository.Setup(i => i.FindAsync<CompositionPassedCourse>(GetTrueSpecification<CompositionPassedCourse>(), null))
                .Returns(Task.FromResult(new CompositionPassedCourse()));
            repository.Setup(i => i.GetAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null))
                .Returns(Task.FromResult( new List<CompositionPassedMaterial>() {
                            new CompositionPassedMaterial {  UserId = 2, MaterialId =0 },
                            new CompositionPassedMaterial {  UserId = 2, MaterialId = 5},
                    }.AsEnumerable()));

            CourseProgressDTO expected = new CourseProgressDTO()
            {
                Id = 1,
                MaterialsPassed = new List<MaterialDTO> { new BookMaterialDTO() { Id = 5 } },
                MaterialsNotPassed = new List<MaterialDTO>
                {
                    new VideoMaterialDTO() {Id = 1},
                    new ArticleMaterialDTO(){Id = 2},
                    new ArticleMaterialDTO(){Id = 3}
                }

            };

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourseAsync(1, 2);

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.MaterialsPassed.Count, result.Result.MaterialsPassed.Count);
            Assert.AreEqual(expected.MaterialsNotPassed.Count, result.Result.MaterialsNotPassed.Count);
        }

        [TestMethod]
        public void GetProgressCourses_UserDontHaveAny_ReturnsEmptyArrayOfCourseProgressDTO()
        {
            repository.Setup(i => i.GetAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null))
                .Returns(Task.FromResult(new List<CompositionPassedMaterial>()
                {
                }.AsEnumerable()));

            int expected = 0;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCoursesAsync(1);

            Assert.AreEqual(expected, result.Result.Count());

        }
 
        [TestMethod]
        public void PassMaterial_UserPassedMaterialBefore_ReturnsMaterialWithId5()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new CourseDTO() { Materials = new List<MaterialDTO>() }));

            repository.Setup(i => i.FindAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null))
                .Returns(Task.FromResult(new CompositionPassedMaterial() { MaterialId = 5 }));
            MaterialDTO m = new VideoMaterialDTO() { Id = 5 };
            serviceMaterial.Setup(i => i.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(m));

            VideoMaterialDTO expected = new VideoMaterialDTO { Id = 5 };

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.PassMaterialAsync(It.IsAny<int>(), It.IsAny<int>(), 5);

            Assert.AreEqual(expected.Id, result.Id);
        }

        [TestMethod]
        public void PassMaterial_UserPassedDidntMaterialId5Before_ReturnsMaterial()
        {
            serviceCourse.Setup(i => i.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new CourseDTO() { Materials = new List<MaterialDTO>(), MaterialsId = new List<int>() { 5 }, Skills = new List<SkillDTO>() }));

            repository.Setup(i => i.FindAsync<CompositionPassedMaterial>(GetTrueSpecification<CompositionPassedMaterial>(), null))
                .Returns(Task.FromResult(new CompositionPassedMaterial() { MaterialId = 0 }));

            MaterialDTO m = new VideoMaterialDTO() { Id = 5 };
            serviceMaterial.Setup(i => i.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(m));

            VideoMaterialDTO expected = new VideoMaterialDTO { Id = 5 };

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.PassMaterialAsync(It.IsAny<int>(), It.IsAny<int>(), 5);

            Assert.AreEqual(expected.Id, result.Id);
        }
    }
}
