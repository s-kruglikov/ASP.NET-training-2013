BEGIN TRAN

CREATE TABLE [dbo].[Posts] (
    [PostId]                   INT            IDENTITY (1, 1) NOT NULL,
    [PostTitle]                NVARCHAR (MAX) NULL,
    [PostDescription]          NVARCHAR (MAX) NULL,
    [PostContent]              VARCHAR (MAX)  NULL,
    [PostCategory]             VARCHAR (MAX)  NULL,
    [PostCreatedBy]            VARCHAR (MAX)  NULL,
    [PostCreationDate]         DATETIME       DEFAULT (getdate()) NULL,
    [PostLastModifiedBy]       VARCHAR (MAX)  NULL,
    [PostLastModificationDate] DATETIME       DEFAULT (getdate()) NULL,
    [PostIsVisible]            BIT            DEFAULT ((1)) NULL,
    [PostCommentsEnabled]      BIT            DEFAULT ((1)) NULL,
    [PostFeatured]             BIT            DEFAULT ((0)) NULL,
    [ImageMimeType]            VARCHAR (50)   NULL,
    [ImageName]                VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([PostId] ASC)
);

CREATE TABLE [dbo].[Comments] (
    [CommentID]                   INT           IDENTITY (1, 1) NOT NULL,
    [PostID]                      INT           NOT NULL,
    [CommentContent]              VARCHAR (MAX) NULL,
    [CommentCreatedBy]            VARCHAR (100) NULL,
    [CommentCreationDate]         DATETIME      DEFAULT (getdate()) NULL,
    [CommentLastModifiedBy]       VARCHAR (100) NULL,
    [CommentLastModificationDate] DATETIME      CONSTRAINT [DF_Comments_CommentLastModificationDate] DEFAULT (getdate()) NULL,
    [CommentIsVisible]            BIT           CONSTRAINT [DF_Comments_CommentIsVisible] DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([CommentID] ASC),
    CONSTRAINT [FK_Comments_ToPosts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[Posts] ([PostId])
);

CREATE TABLE [dbo].[UserProfile] (
    [UserId]         INT            IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR (56)  NOT NULL,
    [Email]          NVARCHAR (56)  NULL,
    [FirstName]      NVARCHAR (MAX) NULL,
    [LastName]       NVARCHAR (MAX) NULL,
    [BirthDate]      DATE           NULL,
    [Avatar]         NVARCHAR (MAX) NULL,
    [AvatarMimeType] NCHAR (10)     NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            DEFAULT ((0)) NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            DEFAULT ((0)) NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC)
);

CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

IF @@ERROR != 0
BEGIN
	ROLLBACK
END
ELSE
BEGIN
	COMMIT
END