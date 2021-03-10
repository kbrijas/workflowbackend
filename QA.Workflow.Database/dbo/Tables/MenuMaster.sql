CREATE TABLE [dbo].[MenuMaster] (
    [SeqNo]       INT           IDENTITY (1, 1) NOT NULL,
    [Menu]        VARCHAR (500) NOT NULL,
    [ParentSeqNo] INT           NULL,
    [MenuOrder]   INT           NULL,
    [Uisref]      VARCHAR (100) NULL,
    [CreatedOn]   DATETIME      NOT NULL,
    [CreatedBy]   VARCHAR (100) NOT NULL,
    [ModifiedOn]  DATETIME      NULL,
    [ModifiedBy]  VARCHAR (100) NULL,
    [IsActive]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([SeqNo] ASC)
);

