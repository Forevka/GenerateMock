import { AxiosResponse } from 'axios';
import apiClient from '@/api/ApiClient';
import { ICreateRepository } from '@/models/requests/ICreateRepository';
import { IRepository } from '@/models/responses/IRepository';

export default class RepositoryService {
    private repoAxios = apiClient;

    async sendComment(repo: ICreateRepository): Promise<AxiosResponse<IRepository>> {
        return this.repoAxios.post<IRepository>(`Repository`,{}, {params: repo})
    }
}