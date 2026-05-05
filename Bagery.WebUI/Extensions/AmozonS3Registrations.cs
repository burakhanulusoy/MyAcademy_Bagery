using Amazon.Runtime;
using Amazon.S3;

namespace Bagery.WebUI.Extensions
{
    public static class AmozonS3Registrations
    {
        public static void AddAmozonS3Registrations(this IServiceCollection services,IConfiguration configuration)
        {

            var awsOptions = configuration.GetAWSOptions();

            awsOptions.Region = Amazon.RegionEndpoint.EUNorth1;

            awsOptions.Credentials = new BasicAWSCredentials(

                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]


                );

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();



        }


    }
}
