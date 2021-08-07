import { RinkLineTemplatePage } from './app.po';

describe('RinkLine App', function() {
  let page: RinkLineTemplatePage;

  beforeEach(() => {
    page = new RinkLineTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
