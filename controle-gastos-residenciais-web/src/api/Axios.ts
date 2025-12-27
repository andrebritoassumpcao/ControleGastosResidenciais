import { BaseClient } from "@/base/BaseClient";

class AxiosClient extends BaseClient {
  public get = this.client.get;
  public post = this.client.post;
  public put = this.client.put;
  public delete = this.client.delete;
  public patch = this.client.patch;
}

export const AxiosAPI = new AxiosClient();
