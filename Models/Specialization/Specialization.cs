using TodoApi.Models.Shared;

namespace TodoApi.Models.Specialization
{
    public class Specialization : Entity<SpecializationId>
        {

            public SpecializationDesignation SpecializationDesignation{ get;  private set; }
            public SpecializationCode SpecializationCode{get; private set;}
            public SpecializationDescription SpecializationDescription{get; private set;}

            private Specialization()
            {
            }

            public Specialization(SpecializationDesignation specializationDesignation,SpecializationCode specializationCode,SpecializationDescription specializationDescription)
            {
                this.Id = new SpecializationId(Guid.NewGuid());
                this.SpecializationDesignation = specializationDesignation;
                this.SpecializationCode=specializationCode;
                this.SpecializationDescription=specializationDescription;
            }



            public void ChangeSpecializationDesignation(SpecializationDesignation designation)
            {
                this.SpecializationDesignation = designation;
            }

            public void ChangeSpecializationCode(SpecializationCode code)
            {
                this.SpecializationCode = code;
            }

            public void UpdateSpecializationDescription(SpecializationDescription specializationDescription){
                this.SpecializationDescription=specializationDescription;
            }
        }
}