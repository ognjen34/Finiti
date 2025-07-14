using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finiti.DATA.Migrations
{
    /// <inheritdoc />
    public partial class filldata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Authors",
            columns: new[] { "Id", "FirstName", "LastName", "Username", "Password", "RoleId" },
            values: new object[,]
            {
                { 0, "John", "Doe", "john.doe", "password1", 0 },
                { 1, "Jane", "Smith", "jane.smith", "password2", 0 },
                { 2, "Alice", "Johnson", "alice.j", "password3", 0 },
                { 3, "Bob", "Brown", "bob.brown", "password4", 0 },
                { 4, "Charlie", "Davis", "charlie.d", "password5", 0 },
                { 5, "Diana", "Evans", "diana.evans", "password6", 0 },
                { 6, "Evan", "Foster", "evan.foster", "password7", 0 },
                { 7, "Fiona", "Green", "fiona.green", "password8", 0 },
                { 8, "George", "Hill", "george.hill", "password9", 0 },
                { 9, "Hannah", "Irwin", "hannah.irwin", "password10", 0 }
            });

            migrationBuilder.InsertData(
            table: "GlossaryTerms",
            columns: new[] { "Id", "Term", "Definition", "CreatedAt", "AuthorId", "Status", "IsDeleted" },
            values: new object[,]
            {
                { 0, "Polymorphism", "Ability of objects to take many forms", DateTime.UtcNow, 1, 1, false },
                { 1, "Encapsulation", "Hiding internal state and requiring all interaction", DateTime.UtcNow, 2, 0, false },
                { 2, "Abstraction", "Simplifying complex reality by modeling classes", DateTime.UtcNow, 3, 1, false },
                { 3, "Inheritance", "Mechanism for a class to derive properties from another", DateTime.UtcNow, 4, 1, false },
                { 4, "Interface", "A contract that classes can implement", DateTime.UtcNow, 5, 0, false },
                { 5, "Algorithm", "Step-by-step procedure for calculations", DateTime.UtcNow, 1, 1, false },
                { 6, "Array", "Collection of elements identified by index", DateTime.UtcNow, 2, 0, false },
                { 7, "Stack", "Data structure with LIFO ordering", DateTime.UtcNow, 3, 1, false },
                { 8, "Queue", "Data structure with FIFO ordering", DateTime.UtcNow, 4, 1, false },
                { 9, "Recursion", "Function calling itself to solve problems", DateTime.UtcNow, 5, 0, false },
                { 10, "Hash Table", "Data structure mapping keys to values", DateTime.UtcNow, 1, 1, false },
                { 11, "Binary Tree", "Hierarchical data structure with nodes", DateTime.UtcNow, 2, 0, false },
                { 12, "Graph", "Collection of nodes connected by edges", DateTime.UtcNow, 3, 1, false },
                { 13, "JSON", "Lightweight data-interchange format", DateTime.UtcNow, 4, 1, false },
                { 14, "XML", "Markup language for data representation", DateTime.UtcNow, 5, 0, false },
                { 15, "API", "Interface for software interaction", DateTime.UtcNow, 1, 1, false },
                { 16, "Dependency Injection", "Technique to achieve inversion of control", DateTime.UtcNow, 2, 0, false },
                { 17, "Unit Test", "Testing smallest parts of code", DateTime.UtcNow, 3, 1, false },
                { 18, "Integration Test", "Testing combined parts of application", DateTime.UtcNow, 4, 1, false },
                { 19, "Continuous Integration", "Practice of merging code frequently", DateTime.UtcNow, 5, 0, false },
                { 20, "Middleware", "Software layer between OS and applications", DateTime.UtcNow, 1, 1, false },
                { 21, "ORM", "Object-relational mapping", DateTime.UtcNow, 2, 0, false },
                { 22, "MVC", "Model-View-Controller architecture", DateTime.UtcNow, 3, 1, false },
                { 23, "REST", "Representational State Transfer", DateTime.UtcNow, 4, 1, false },
                { 24, "SOAP", "Protocol for exchanging structured info", DateTime.UtcNow, 5, 0, false },
                { 25, "JWT", "JSON Web Token for authentication", DateTime.UtcNow, 1, 1, false },
                { 26, "OAuth", "Open authorization standard", DateTime.UtcNow, 2, 0, false },
                { 27, "NoSQL", "Non-relational databases", DateTime.UtcNow, 3, 1, false },
                { 28, "SQL", "Structured Query Language", DateTime.UtcNow, 4, 1, false },
                { 29, "Cloud Computing", "Delivery of computing services via internet", DateTime.UtcNow, 5, 0, false },
                { 30, "Microservices", "Architectural style for applications", DateTime.UtcNow, 1, 1, false },
                { 31, "Docker", "Container platform", DateTime.UtcNow, 2, 0, false },
                { 32, "Kubernetes", "Container orchestration system", DateTime.UtcNow, 3, 1, false },
                { 33, "Agile", "Iterative development methodology", DateTime.UtcNow, 4, 1, false },
                { 34, "Scrum", "Agile framework", DateTime.UtcNow, 5, 0, false },
                { 35, "Kanban", "Visual workflow management", DateTime.UtcNow, 1, 1, false },
                { 36, "DevOps", "Development and operations practices", DateTime.UtcNow, 2, 0, false },
                { 37, "CI/CD", "Continuous Integration/Continuous Delivery", DateTime.UtcNow, 3, 1, false },
                { 38, "Virtual Machine", "Software emulation of a physical computer", DateTime.UtcNow, 4, 1, false },
                { 39, "Hypervisor", "Virtual machine manager", DateTime.UtcNow, 5, 0, false },
                { 40, "Big Data", "Large and complex data sets", DateTime.UtcNow, 1, 1, false },
                { 41, "Machine Learning", "Algorithms that improve from experience", DateTime.UtcNow, 2, 0, false },
                { 42, "Artificial Intelligence", "Simulation of human intelligence", DateTime.UtcNow, 3, 1, false },
                { 43, "Blockchain", "Distributed ledger technology", DateTime.UtcNow, 4, 1, false },
                { 44, "Cryptocurrency", "Digital or virtual currency", DateTime.UtcNow, 5, 0, false },
                { 45, "Cybersecurity", "Protecting computer systems", DateTime.UtcNow, 1, 1, false },
                { 46, "Encryption", "Encoding data to prevent access", DateTime.UtcNow, 2, 0, false },
                { 47, "Decryption", "Decoding encrypted data", DateTime.UtcNow, 3, 1, false },
                { 48, "Firewall", "Network security system", DateTime.UtcNow, 4, 1, false },
                { 49, "Load Balancer", "Distributes network or application traffic", DateTime.UtcNow, 5, 0, false },
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
