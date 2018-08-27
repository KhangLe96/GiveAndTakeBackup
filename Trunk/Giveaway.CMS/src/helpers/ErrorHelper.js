import { DEFAULT_ERROR_MESSAGE } from '../common/constants'

const hasException = (response) => {
  return response
    && response.type
    && response.message
    && (response.type.indexOf('Exception') >= 0 || response.type.indexOf('Error') >= 0)
};

const hasExceptionContains = (response, term) => {
  return hasException(response)
    && ((response.message.indexOf(term) >= 0) || (response.type.indexOf(term) >= 0));
};

const extractExceptionMessage = (response) => {
  if (hasException(response)) {
    if (response.message)
      return response.message
    else
      return DEFAULT_ERROR_MESSAGE;
  }
}

const hasError = (response) => {
  return response
    && response.message !== undefined;
};

const ErrorHelper = {
  hasException,
  hasExceptionContains,
  hasError,
  extractExceptionMessage
};
export default ErrorHelper;
