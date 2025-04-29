# AetherLink Indexer: Project Structure and Module Overview

## Overview

AetherLink Indexer is a data indexing and processing framework designed for cross-chain and oracle scenarios in blockchain environments. Built with .NET/C#, it focuses on log event processing, data synchronization, GraphQL queries, and automated testing. The project adopts a modular architecture, supports automatic code generation, and features a comprehensive testing system.

---

## Directory Structure

```
/
├── src/                        # Core source code directory
│   └── AetherLink.Indexer/     # Main project module
│       ├── Processors/         # Core logic: event processors for all main features (see tracker)
│       ├── Common/             # Common utilities and infrastructure
│       ├── Entities/           # Entity and index structure definitions
│       ├── Generated/          # Auto-generated contract and interface code
│       ├── GraphQL/            # GraphQL queries, input/output definitions
│       ├── AetherLinkIndexerAutoMapperProfile.cs  # Mapping configuration
│       ├── AetherLinkIndexerModule.cs             # Module entry point
│       └── ...
├── test/                       # Testing system
│   ├── AetherLink.Indexer.Tests/           # Main test cases
│   ├── AetherLink.Indexer.Orleans.TestBase/ # Orleans test base classes
│   └── AetherLink.Indexer.TestBase/         # Common test base classes
├── docs/                       # Project documentation
│   └── PROJECT_OVERVIEW_AND_STRUCTURE.md    # This structure overview document
├── build.sh / build.ps1         # Build scripts
├── azure-pipelines.yml          # CI configuration
├── README.md                    # Brief project description
└── ...
```

---

## Core Module Overview

### Processors (Core)
- The heart of the project. Implements all main event processing logic as tracked in the project tracker (RequestStarted, Transmitted, ConfigSet, RequestCancelled, etc.).

### Common
- Provides index prefix helpers and general utility infrastructure.

### Entities
- Defines index entities (such as ConfigDigestIndex, OcrJobEventIndex, CommitmentIndex, etc.) for data persistence and querying.

### Generated
- Auto-generated contract interfaces and message definitions, enabling efficient interaction with blockchain contracts.
- Includes files like OracleContract.g.cs, CoordinatorContract.g.cs, etc.

### GraphQL
- Provides GraphQL queries, input types (Input), and data transfer objects (Dtos) for flexible data querying and aggregation.

### AetherLinkIndexerModule & AutoMapperProfile
- Module registration and dependency injection configuration, type mapping definitions.

---

## Auto-Generated Code

- The Generated directory contains *.g.cs and *.c.cs files automatically generated from contract ABIs or proto files, ensuring consistency with on-chain contract interfaces.
- Facilitates rapid iteration and contract upgrades, reducing manual maintenance costs.

---

## Testing System

- test/AetherLink.Indexer.Tests/: Main test cases covering core business logic and processors.
- test/AetherLink.Indexer.Orleans.TestBase/: Orleans test base classes for distributed and integration testing.
- test/AetherLink.Indexer.TestBase/: Common test base classes for high reusability.
- The test directories also include auto-generated contract interaction code to ensure consistency between tests and main business logic.

---

## CI and Documentation

- azure-pipelines.yml: Continuous integration configuration for automated testing and builds.
- docs/: Project documentation directory, can be extended with module docs, design docs, etc.
- README.md: Brief project description.

---

_This file is auto-generated and will be updated automatically as the project structure and modules evolve._ 