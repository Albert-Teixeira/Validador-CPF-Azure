using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace httpValidaCpf
{
    public static class fnvalidacpf
    {
        [FunctionName("fnvalidacpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Iniciando a validação do CPF.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data == null)
            {
                return new BadRequestObjectResult("Por favor, informe o CPF.");
            }
            string cpf = data?.cpf;
            if (string.IsNullOrEmpty(cpf))
            {
                return new BadRequestObjectResult("Por favor, informe o CPF.");
            }
            bool isValid = Validacpf(cpf);
            string responseMessage = isValid ? "CPF válido e não consta na base de dados de fraudes" : "CPF inválido.";
            return new OkObjectResult(responseMessage);
        }

        public static bool Validacpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum1 = 0;
            for (int i = 0; i < 9; i++)
            {
                sum1 += (cpf[i] - '0') * weights1[i];
            }
            int check1 = sum1 % 11 < 2 ? 0 : 11 - (sum1 % 11);
            if (check1 != (cpf[9] - '0'))
                return false;

            int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum2 = 0;
            for (int i = 0; i < 10; i++)
            {
                sum2 += (cpf[i] - '0') * weights2[i];
            }
            int check2 = sum2 % 11 < 2 ? 0 : 11 - (sum2 % 11);
            if (check2 != (cpf[10] - '0'))
                return false;

            return true;
        }
    }
}