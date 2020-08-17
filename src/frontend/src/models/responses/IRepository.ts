export interface IRepository {
    repositoryId: string;
    repositoryName: string;
    ownerId: string;
    repositoryDatabase: IRepositoryDatabase[];
}

export interface IRepositoryDatabase {
    repositoryId: string;
    databaseId: string;
    databaseFilePath: string;
    databaseLoadTime: Date;
    databaseVersion: number;
    databaseApiUrl: string;
}
