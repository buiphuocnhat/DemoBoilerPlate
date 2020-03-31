import { Project1TemplatePage } from './app.po';

describe('Project1 App', function() {
  let page: Project1TemplatePage;

  beforeEach(() => {
    page = new Project1TemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
